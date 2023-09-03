using MAUI.MemoryLeaks.Model;

namespace MAUI.MemoryLeaks.ViewModel;

public partial class Case01ViewModel : BaseViewModel
{
    public Case01ViewModel()
    {
        PageName = "Case 1";
        Description =
            "Note down the memory used. Add the items, wait for the UI to populate and then clear the items again. Now wait for few minutes and observe." + Environment.NewLine + Environment.NewLine +
            "Even after minutes of waiting you will find the memory consumption approx. 20-30 MB higher then before. It will be freed up first after you leave this page and navigate back to home screen.";
    }

    [ObservableProperty]
    private string _itemsCount;

    [ObservableProperty]
    private ObservableCollectionEx<ItemSample> _items = new();

    [RelayCommand]
    private void AddItems()
    {
        IsBusy = true;
        var newItems = new List<ItemSample>(TestItemsCollectionCount);
        for (var i = 0; i < TestItemsCollectionCount; i++) newItems.Add(new ItemSample());
        Items.AddRange(newItems);
        IsBusy = false;
    }

    [RelayCommand]
    private void ClearItems()
    {
        IsBusy = true;
        Items.Clear();
        // The following line will also 'fix' the memory leak.
        //Items = new ObservableCollectionEx<ItemSample>();
        IsBusy = false;
    }

    protected override void RefreshInfo()
    {
        base.RefreshInfo();
        ItemsCount = $"Items count: {Items.Count}";
    }
}