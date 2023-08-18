using System.Collections.Specialized;

namespace MAUI.MemoryLeaks;

public class ObservableList<T> : List<T>, INotifyCollectionChanged
{
    public event NotifyCollectionChangedEventHandler CollectionChanged;

    public void Notify()
    {
        CollectionChanged?.Invoke(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
    }
}