using MAUI.MemoryLeaks.Extensions;
using MAUI.MemoryLeaks.Model;

namespace MAUI.MemoryLeaks.ViewModel;

public partial class Case02SolutionViewModel : BaseViewModel
{
    public Case02SolutionViewModel()
    {
        PageName = "Case 2: Solution / Workaround";
        Recommendation = RecommendationAddItems;
        Description =
            "Compared to the first flow the memory will be freed up without having to leave this page. Just wait long enough, i.e. even minutes." +
            $"{Environment.NewLine}{Environment.NewLine}" +
            "It is because ObservableCollection is re-created upon each clear. See the corresponding code. That is however just a workaround IMHO.";
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