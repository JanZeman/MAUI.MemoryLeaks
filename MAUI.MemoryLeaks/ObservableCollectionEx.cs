using System.Collections.ObjectModel;
using System.Collections.Specialized;

namespace MAUI.MemoryLeaks;

/// <summary>
/// The ordinary ObservableCollection with some helper methods.
/// </summary>
/// <typeparam name="T"></typeparam>
public class ObservableCollectionEx<T> : ObservableCollection<T>
{
    private bool _suppressNotification;

    public ObservableCollectionEx() { }

    public ObservableCollectionEx(IEnumerable<T> collection) : base(collection) { }

    public ObservableCollectionEx(List<T> list) : base(list) { }

    protected override void OnCollectionChanged(NotifyCollectionChangedEventArgs e)
    {
        if (!_suppressNotification)
            base.OnCollectionChanged(e);
    }

    public void AddRange(IEnumerable<T> enumerable)
    {
        var list = enumerable as IList<T> ?? enumerable?.ToList();
        if (list == null || list.Count == 0) return;

        CheckReentrancy();

        _suppressNotification = true;

        foreach (var item in list)
            Add(item);

        _suppressNotification = false;

        OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
    }

    public void RemoveAll(Func<T, bool> predicate)
    {
        if (Count == 0) return;

        CheckReentrancy();

        _suppressNotification = true;

        var removedItems = this.Where(predicate).ToList();
        foreach (var item in removedItems)
            Remove(item);

        _suppressNotification = false;

        if (removedItems.Count > 0)
            OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Remove, removedItems));
    }


    /// <summary>
    /// Attempt of Clear() alternative, no any visible behavior differences so unused for now.
    /// </summary>
    public void ClearCollection()
    {
        if (Count == 0) return;

        _suppressNotification = true;

        ClearItems(); // Calls CheckReentrancy() internally

        _suppressNotification = false;

        OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
    }
}
