using MAUI.MemoryLeaks.Model;

namespace MAUI.MemoryLeaks.ViewModel;

public partial class Case01ViewModel : BaseViewModel
{
    private const int ItemsToAddEachRound = 1000000;

    [ObservableProperty]
    private string _itemsCount;

    [ObservableProperty]
    private ObservableInheritedCollection<ItemSample> _items = new();

    [RelayCommand]
    private void AddItems()
    {
        var newItems = new List<ItemSample>(ItemsToAddEachRound);
        for (var i = 0; i < ItemsToAddEachRound; i++) newItems.Add(new ItemSample());
        Items.AddRange(newItems);
    }

    [RelayCommand]
    private void ClearItems()
    {
        Items.Clear();
        // The following line will also 'fix' the memory leak.
        //Items = new ObservableInheritedCollection<ItemSample>();
    }

    protected override void RefreshInfo()
    {
        base.RefreshInfo();
        ItemsCount = $"Items count: {Items.Count}";
    }
}