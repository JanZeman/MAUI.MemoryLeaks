using System.Collections.ObjectModel;
using System.Collections.Specialized;

namespace MAUI.MemoryLeaks;

public class ObservableInheritedCollection<T> : ObservableCollection<T>
{
    private bool _suppressNotification = false;

    public ObservableInheritedCollection() : base() { }

    public ObservableInheritedCollection(IEnumerable<T> collection) : base(collection) { }

    public ObservableInheritedCollection(List<T> list) : base(list) { }

    protected override void OnCollectionChanged(NotifyCollectionChangedEventArgs e)
    {
        if (!_suppressNotification)
            base.OnCollectionChanged(e);
    }

    public void AddRange(IEnumerable<T> collection)
    {
        ArgumentNullException.ThrowIfNull(collection);

        _suppressNotification = true;

        foreach (var item in collection)
            Add(item);

        _suppressNotification = false;
        OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
    }
}
