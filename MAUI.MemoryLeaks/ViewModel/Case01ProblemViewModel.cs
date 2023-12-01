namespace MAUI.MemoryLeaks.ViewModel;

public class Case01ProblemViewModel : ItemsViewModel
{
    public Case01ProblemViewModel()
    {
        PageName = "Case 1: Problem";
        Recommendation = RecommendationAddItems;
        Description =
            "Even after minutes of waiting you will find the memory usage approx. 20-30 MB higher then before. " +
            "It will be freed up first after you leave this page and navigate one level up." +
            "That must probably mean that the memory is freed first after the CollectionView control is completely destroyed; not earlier when all its items got destroyed.";

        UseCase01Workaround = false;
    }
}