using System.Collections.Specialized;

namespace MAUI.MemoryLeaks;

public class SimpleObservableCollection<T> : List<T>, INotifyCollectionChanged
{
    public SimpleObservableCollection() { }

    public SimpleObservableCollection(IEnumerable<T> collection)
    {
        AddRange(collection);
        Notify();
    }

    public event NotifyCollectionChangedEventHandler? CollectionChanged;

    public void Notify()
    {
        CollectionChanged?.Invoke(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
    }
}