namespace MAUI.MemoryLeaks.ViewModel;

public class Case02SolutionViewModel : ItemsViewModel
{
    public Case02SolutionViewModel()
    {
        PageName = "Case 2: Solution / Workaround";
        Recommendation = RecommendationAddItems;
        Description =
            "Even after long minutes of waiting you will find the memory usage approx. 50-70 MB higher then before. " +
            "It is a lot of memory and it will not be freed up unless you quit the app." +
            $"{Environment.NewLine}{Environment.NewLine}" +
            "This can be considered as a showstopper for any application showing non-minimalistic number of items. I haven't found any solution or workaround yet. ";

        AddedItemsCount = 1000;
        RecommendationClearItems = "The UI will fully load after more than 30 seconds. For only 1.000 items! Try scrolling on the CollectionView below, wait for it, probably it is still loading...";
        RecommendationObserve = "Wait for few minutes and observe the memory behavior.";
    }
}