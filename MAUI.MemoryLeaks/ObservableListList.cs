using System.Collections.Specialized;

namespace MAUI.MemoryLeaks;

public class ObservableListList<T> : List<T>, INotifyCollectionChanged
{
    public event NotifyCollectionChangedEventHandler CollectionChanged;

    public new void Add(T item)
    {
        base.Add(item);
        OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add, item));
    }

    public new void AddRange(IEnumerable<T> collection)
    {
        base.AddRange(collection);
        // This assumes that the entire collection is changed when you add a range.
        OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
    }

    public new void Clear()
    {
        base.Clear();
        OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
    }

    public new void Insert(int index, T item)
    {
        base.Insert(index, item);
        OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add, item, index));
    }

    public new bool Remove(T item)
    {
        int index = base.IndexOf(item);
        bool removed = base.Remove(item);
        if (removed)
        {
            OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Remove, item, index));
        }
        return removed;
    }

    public new void RemoveAt(int index)
    {
        T removedItem = base[index];
        base.RemoveAt(index);
        OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Remove, removedItem, index));
    }

    protected virtual void OnCollectionChanged(NotifyCollectionChangedEventArgs e)
    {
        CollectionChanged?.Invoke(this, e);
    }
}
