using System.Collections.Specialized;
using System.Collections;

namespace MAUI.MemoryLeaks;

/// <summary>
/// An attempt to implement a performing lightweight ObservableList that can easily hold 1.000.000 items and still bind to MAUI CollectionView.
/// The .NET ObservableCollection leaks memory until new ObservableCollection is created and bound to UI control.
/// This version is based on composition around and instance of internal generic list.
/// If non-generic IList interface not implemented then MAUI CollectionView does not show any items. What exact property or method causes this is unknown to me at the moment.
/// </summary>
public class ObservableList<T> : IList<T>, IList, INotifyCollectionChanged
{
    private List<T> _internalList = new();

    public ObservableList() { }

    public ObservableList(IEnumerable<T> collection)
    {
        AddRange(collection);
    }

    public event NotifyCollectionChangedEventHandler CollectionChanged;

    // IList<T> members
    public T this[int index]
    {
        get => _internalList[index];
        set
        {
            _internalList[index] = value;
            OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Replace, value, index));
        }
    }

    public int Count => _internalList.Count;

    public bool IsReadOnly => false;

    public void Add(T item)
    {
        _internalList.Add(item);
        OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add, item));
    }

    public void Clear()
    {
        _internalList.Clear();
        // The next line should not be necessary but it avoids memory leaks on MAUI Windows application.
        // Question is what is the root cause, Microsoft.Maui.Controls.Handlers.Items.CollectionViewHandler maybe?
        _internalList = new List<T>();
        OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
    }

    public bool Contains(T item) => _internalList.Contains(item);

    public void CopyTo(T[] array, int arrayIndex) => _internalList.CopyTo(array, arrayIndex);

    public IEnumerator<T> GetEnumerator() => _internalList.GetEnumerator();

    public int IndexOf(T item) => _internalList.IndexOf(item);

    public void Insert(int index, T item)
    {
        _internalList.Insert(index, item);
        OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add, item, index));
    }

    public bool Remove(T item)
    {
        var index = _internalList.IndexOf(item);
        if (!_internalList.Remove(item)) return false;
        OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Remove, item, index));
        return true;
    }

    public void RemoveAt(int index)
    {
        var removedItem = _internalList.ElementAt(index);
        _internalList.RemoveAt(index);
        OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Remove, removedItem, index));
    }

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    // IList members (explicitly implemented)
    object IList.this[int index]
    {
        get => _internalList[index];
        set => _internalList[index] = (T)value;
    }

    public bool IsFixedSize => false;

    public object SyncRoot => ((IList)_internalList).SyncRoot;

    public bool IsSynchronized => ((IList)_internalList).IsSynchronized;

    public int Add(object value)
    {
        Add((T)value);
        return IndexOf((T)value);
    }

    public bool Contains(object value) => ((IList)_internalList).Contains(value);

    public int IndexOf(object value) => ((IList)_internalList).IndexOf(value);

    public void Insert(int index, object value)
    {
        Insert(index, (T)value);
    }

    public void Remove(object value)
    {
        Remove((T)value);
    }

    public void CopyTo(Array array, int index)
    {
        ((IList)_internalList).CopyTo(array, index);
    }

    private void OnCollectionChanged(NotifyCollectionChangedEventArgs e)
    {
        CollectionChanged?.Invoke(this, e);
    }

    // Method to add a range to the list (used in the constructor)
    public void AddRange(IEnumerable<T> collection)
    {
        var itemsToAdd = collection?.ToList() ?? new List<T>();
        if (itemsToAdd.Count == 0) return;

        var startingIndex = _internalList.Count;
        _internalList.AddRange(itemsToAdd);
        OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add, itemsToAdd, startingIndex));
    }
}
