using MAUI.MemoryLeaks.Model;

namespace MAUI.MemoryLeaks.ViewModel;

public partial class Case01Workaround02ViewModel : BaseViewModel
{
    public Case01Workaround02ViewModel()
    {
        PageName = "Case 1, Workaround 2";
        Description =
            "Same as for the first workaround the memory will be freed up without having to lave this page." + Environment.NewLine + Environment.NewLine +
            "It is because ObservableList is used which automatically re-assigns its internal list upon each clear. That is however also just an workaround IMHO.";
    }

    [ObservableProperty]
    private string _itemsCount;

    [ObservableProperty]
    private ObservableList<ItemSample> _items = new();

    [RelayCommand]
    private void AddItems()
    {
        var newItems = new List<ItemSample>(TestItemsCollectionCount);
        for (var i = 0; i < TestItemsCollectionCount; i++) newItems.Add(new ItemSample());
        Items.AddRange(newItems);
    }

    [RelayCommand]
    private void ClearItems()
    {
        Items.Clear();
        // No any additional code here as the same kind of workaround is implemented within the ObservableList class.
    }

    protected override void RefreshInfo()
    {
        base.RefreshInfo();
        ItemsCount = $"Items count: {Items.Count}";
    }
}