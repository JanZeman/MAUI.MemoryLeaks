namespace MAUI.MemoryLeaks.ViewModel;

public class Case01SolutionViewModel : ItemsViewModel
{
    public Case01SolutionViewModel()
    {
        PageName = "Case 1: Solution / Workaround";
        Recommendation = RecommendationAddItems;
        Description =
            "Compared to the first flow the memory will be freed up without having to leave this page. Just wait long enough, i.e. few minutes." +
            $"{Environment.NewLine}{Environment.NewLine}" +
            "It is because ObservableCollection is re-created upon each clear. See the corresponding code. That is however just a workaround IMHO.";
    }
}