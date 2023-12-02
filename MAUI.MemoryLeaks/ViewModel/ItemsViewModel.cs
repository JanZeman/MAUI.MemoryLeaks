using MAUI.MemoryLeaks.Extensions;
using MAUI.MemoryLeaks.Model;

namespace MAUI.MemoryLeaks.ViewModel;

public abstract partial class ItemsViewModel : BaseViewModel
{
    protected const string RecommendationAddItems = "Add items via the button above.";

    protected string RecommendationClearItems = "Wait for UI changes, then clear the items.";
    protected string RecommendationObserve = "Now wait for few minutes and observe the memory behavior.";
    
    protected int AddedItemsCount = 1000 * 1000;
    protected bool UseCase01Workaround = true;

    [ObservableProperty]
    private ObservableCollectionEx<ItemSample> _items = new();

    [ObservableProperty]
    private string _itemsCount;

    [ObservableProperty]
    private bool _itemsNotEmpty;

    [RelayCommand]
    private void AddItems()
    {
        Recommendation = RecommendationClearItems;
        IsBusy = true;

        Task.Run(() =>
        {
            var newItems = new List<ItemSample>(AddedItemsCount);
            for (var i = 0; i < AddedItemsCount; i++)
                newItems.Add(new ItemSample());

            MainThread.InvokeOnMainThreadAsync(() =>
            {
                Items.AddRange(newItems);
                IsBusy = false;
                RefreshInfo();
            });
        });
    }

    [RelayCommand]
    private void ClearItems()
    {
        if (Items.Count < 1) return;

        Recommendation = RecommendationObserve;
        IsBusy = true;

        Task.Run(() =>
        {
            MainThread.InvokeOnMainThreadAsync(() =>
            {
                Items.Clear();

                if (UseCase01Workaround)
                {
                    // The next line is necessary to avoid memory leaks of the 'Case 1' type.
                    // The real fix must probably be done in .NET code for ObservableCollection<T> class.
                    Items = new();
                }

                IsBusy = false;
                RefreshInfo();
            });
        });
    }

    protected override void RefreshInfo()
    {
        base.RefreshInfo();
        var itemsCount = Items.Count;
        ItemsCount = $"Items count: {itemsCount.FormatWithDots()}";
        ItemsNotEmpty = itemsCount > 0;
    }
}