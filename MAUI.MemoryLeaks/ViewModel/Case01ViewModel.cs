using MAUI.MemoryLeaks.Model;

namespace MAUI.MemoryLeaks.ViewModel;

public partial class Case01ViewModel : BaseViewModel
{
    [ObservableProperty]
    private string _itemsCount;

    [ObservableProperty]
    private SimpleObservableCollection<Item> _items = new();

    [RelayCommand]
    private void AddItems()
    {
        const int count = 100000;
        var newItems = new List<Item>(count);
        for (var i = 0; i < count; i++) newItems.Add(new Item());
        Items.AddRange(newItems);

        UpdateInfo();
    }

    [RelayCommand]
    private void ClearItems()
    {
        Items.Clear();
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