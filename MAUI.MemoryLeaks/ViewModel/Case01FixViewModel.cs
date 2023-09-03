using MAUI.MemoryLeaks.Model;

namespace MAUI.MemoryLeaks.ViewModel;

public partial class Case01FixViewModel : BaseViewModel
{
    private const int ItemsToAddEachRound = 1000000;

    public Case01FixViewModel()
    {
        PageName = "Case 1 Fix";
    }

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
        // The next line should not be necessary but it avoids memory leaks on MAUI Windows application.
        // Question is what is the root cause, Microsoft.Maui.Controls.Handlers.Items.CollectionViewHandler maybe?
        Items = new ObservableInheritedCollection<ItemSample>();
    }

    protected override void RefreshInfo()
    {
        base.RefreshInfo();
        ItemsCount = $"Items count: {Items.Count}";
    }
}