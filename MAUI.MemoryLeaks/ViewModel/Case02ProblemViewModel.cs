using MAUI.MemoryLeaks.Extensions;
using MAUI.MemoryLeaks.Model;

namespace MAUI.MemoryLeaks.ViewModel;

public partial class Case02ProblemViewModel : BaseViewModel
{
    public Case02ProblemViewModel()
    {
        PageName = "Case 2: Problem";
        Recommendation = RecommendationAddItems;
        Description =
            "Even after long minutes of waiting you will find the memory usage approx. 50-70 MB higher then before. " +
            "It is that much memory and it will not be freed up unless you quit the app." +
            $"{Environment.NewLine}{Environment.NewLine}" +
            "This can be considered as a showstopper for any application showing non-minimalistic number of items. I haven't found any solution or workaround yet. ";
    }

    [ObservableProperty]
    private string _itemsCount;

    [ObservableProperty]
    private ObservableCollectionEx<ItemSample> _items = new();

    [RelayCommand]
    private void AddItems()
    {
        Recommendation = RecommendationClearItemsCase02;
        IsBusy = true;

        Task.Run(() =>
        {
            var newItems = new List<ItemSample>(ItemsCountSmall);
            for (var i = 0; i < ItemsCountSmall; i++)
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

        Recommendation = RecommendationObserveCase02;
        IsBusy = true;

        Task.Run(() =>
        {
            MainThread.InvokeOnMainThreadAsync(() =>
            {
                Items.Clear();

                // The workaround from the Case 1 does not prevent leak of Case 2 type
                Items = new();
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