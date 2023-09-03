using System.Collections.ObjectModel;
using System.Collections.Specialized;

namespace MAUI.MemoryLeaks;

/// <summary>
/// The ordinary ObservableCollection with AddRange() method.
/// </summary>
/// <typeparam name="T"></typeparam>
public class ObservableCollectionEx<T> : ObservableCollection<T>
{
    private bool _suppressNotification = false;

    public ObservableCollectionEx() : base() { }

    public ObservableCollectionEx(IEnumerable<T> collection) : base(collection) { }

    public ObservableCollectionEx(List<T> list) : base(list) { }

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
