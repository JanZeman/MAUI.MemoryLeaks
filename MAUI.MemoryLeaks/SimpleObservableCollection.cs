using System.Collections.Specialized;

namespace MAUI.MemoryLeaks;

public class SimpleObservableCollection<T> : IList<T>, INotifyCollectionChanged
{
    private List<T> _internalList = new ();

    public SimpleObservableCollection() { }

    public SimpleObservableCollection(IEnumerable<T> collection)
    {
        AddRange(collection);
    }

    public event NotifyCollectionChangedEventHandler CollectionChanged;

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

    public void AddRange(IEnumerable<T> collection)
    {
        var itemsToAdd = collection?.ToList() ?? new List<T>();
        if (itemsToAdd.Count == 0) return;

        var startingIndex = _internalList.Count;
        _internalList.AddRange(itemsToAdd);

        // Notify about the added items in one go.
        OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add, itemsToAdd, startingIndex));
    }

    public void Clear()
    {
        _internalList.Clear();
        // This next line should not be necessary but it avoids memory leaks on Windows application.
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

    System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() => GetEnumerator();

    private void OnCollectionChanged(NotifyCollectionChangedEventArgs e)
    {
        CollectionChanged?.Invoke(this, e);
    }
}