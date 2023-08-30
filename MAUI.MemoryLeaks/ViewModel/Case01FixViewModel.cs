using MAUI.MemoryLeaks.Model;

namespace MAUI.MemoryLeaks.ViewModel;

public partial class Case01FixViewModel : BaseViewModel
{
    private const int ItemsToAddEachRound = 1000000;

    [ObservableProperty]
    private string _itemsCount;

    [ObservableProperty]
    private ObservableInheritedList<ItemSample> _items = new();

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
    }

    protected override void RefreshInfo()
    {
        base.RefreshInfo();
        ItemsCount = $"Items count: {Items.Count}";
    }
}