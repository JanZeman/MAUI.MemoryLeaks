using MAUI.MemoryLeaks.Model;

namespace MAUI.MemoryLeaks.ViewModel;

public partial class Case01ViewModel : BaseViewModel
{
    [ObservableProperty]
    private string _itemsCount;

    [ObservableProperty]
    private ObservableList<ItemSample> _items = new();

    [RelayCommand]
    private void AddItems()
    {
        const int count = 100000;
        var newItems = new List<ItemSample>(count);
        for (var i = 0; i < count; i++) newItems.Add(new ItemSample());
        Items.AddRange(newItems);
        //Items.Notify();

        UpdateInfo();
    }

    [RelayCommand]
    private void ClearItems()
    {
        Items.Clear();
        Items = new ObservableList<ItemSample>();
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