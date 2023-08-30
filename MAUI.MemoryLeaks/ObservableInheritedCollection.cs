using System.Collections.ObjectModel;
using System.Collections.Specialized;

namespace MAUI.MemoryLeaks;

/// <summary>
/// The ordinary ObservableCollection with AddRange() method.
/// </summary>
/// <typeparam name="T"></typeparam>
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

    public void AddRange(IEnumerable<T> enumerable)
    {
        if (enumerable == null) return;
        var list = enumerable.ToList();
        if (list.Count == 0) return;

        _suppressNotification = true;

        foreach (var item in list)
            Add(item);

        _suppressNotification = false;

        OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
    }
}
