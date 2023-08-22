using MAUI.MemoryLeaks.Model;

namespace MAUI.MemoryLeaks.ViewModel;

public partial class Case01ViewModel : BaseViewModel
{
    [ObservableProperty]
    private string _itemsCount;

    [ObservableProperty]
    private ObservableComposedList<ItemSample> _items = new();

    [RelayCommand]
    private void AddItems()
    {
        const int count = 1000000;
        var newItems = new List<ItemSample>(count);
        for (var i = 0; i < count; i++) newItems.Add(new ItemSample());
        Items.AddRange(newItems);
        UpdateInfo();
    }

    [RelayCommand]
    private void ClearItems()
    {
        Items.Clear();
        // The following line is required to avoids memory leaks in case other class than ObservableComposedList is used.
        //Items = new ObservableInheritedCollection<ItemSample>();
        UpdateInfo();
    }

    protected override void UpdateInfo()
    {
        base.UpdateInfo();
        ItemsCount = UpdateItemsCount();
    }

    private string UpdateItemsCount()
    {
        return $"Items count: {Items.Count}";
    }
}