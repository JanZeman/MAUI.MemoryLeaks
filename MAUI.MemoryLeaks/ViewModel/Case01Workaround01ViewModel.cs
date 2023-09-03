using MAUI.MemoryLeaks.Model;

namespace MAUI.MemoryLeaks.ViewModel;

public partial class Case01Workaround01ViewModel : BaseViewModel
{
    public Case01Workaround01ViewModel()
    {
        PageName = "Case 1, Workaround 1";
        Description =
            "Compared to the first flow the memory will be freed up without having to lave this page. Just wait long enough, i.e. even minutes." + Environment.NewLine + Environment.NewLine +
            "It is because ObservableCollection is re-created upon each clear. See the corresponding code. That is however just an workaround IMHO.";
    }

    [ObservableProperty]
    private string _itemsCount;

    [ObservableProperty]
    private ObservableCollectionEx<ItemSample> _items = new();

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
        // The next line is necessary to avoid memory leaks on MAUI Windows application.
        // The real fix must probably be done in .NET code for ObservableCollection<T> class.
        Items = new ObservableCollectionEx<ItemSample>();
    }

    protected override void RefreshInfo()
    {
        base.RefreshInfo();
        ItemsCount = $"Items count: {Items.Count}";
    }
}