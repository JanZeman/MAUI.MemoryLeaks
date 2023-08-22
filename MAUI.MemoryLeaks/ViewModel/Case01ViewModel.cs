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
        // The following line can avoid memory leaks if other then ObservableComposedList class is used
        ////Items = new ObservableComposedList<ItemSample>();
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