using MAUI.MemoryLeaks.Extensions;
using MAUI.MemoryLeaks.Model;

namespace MAUI.MemoryLeaks.ViewModel;

public partial class Case01ProblemViewModel : BaseViewModel
{
    public Case01ProblemViewModel()
    {
        PageName = "Case 1: Problem";
        Recommendation = RecommendationAddItems;
        Description =
            "Even after minutes of waiting you will find the memory usage approx. 20-30 MB higher then before. " +
            "It will be freed up first after you leave this page and navigate one level up." +
            "That must probably mean that the memory is freed first after the CollectionView control is completely destroyed; not earlier when all its items got destroyed.";
    }

    [ObservableProperty]
    private string _itemsCount;

    [ObservableProperty]
    private ObservableCollectionEx<ItemSample> _items = new();

    [RelayCommand]
    private void AddItems()
    {
        Recommendation = RecommendationClearItemsCase01;
        IsBusy = true;

        Task.Run(() =>
        {
            var newItems = new List<ItemSample>(ItemsCountLarge);
            for (var i = 0; i < ItemsCountLarge; i++)
                newItems.Add(new ItemSample());

            MainThread.InvokeOnMainThreadAsync(() =>
            {
                Items.AddRange(newItems);
                IsBusy = false;
            });
        });
    }

    [RelayCommand]
    private void ClearItems()
    {
        if (Items.Count < 1) return;

        Recommendation = RecommendationObserveCase01;
        IsBusy = true;

        Task.Run(() =>
        {
            MainThread.InvokeOnMainThreadAsync(() =>
            {
                Items.Clear();
                IsBusy = false;
            });
        });
    }

    protected override void RefreshInfo()
    {
        base.RefreshInfo();
        ItemsCount = $"Items count: {Items.Count.FormatWithDots()}";
    }
}