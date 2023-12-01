namespace MAUI.MemoryLeaks.ViewModel;

public partial class Case01ViewModel : BaseViewModel
{
    public Case01ViewModel()
    {
        PageName = "Case 1: Source reassignment required";
        Recommendation = "Compare the 'Problem' and 'Solution' above";
        Description =
            "CollectionView should, to my best understanding, free up all memory after all its items are removed." + 
            Environment.NewLine + Environment.NewLine +
            "That is however not the case. Please compare the 'Problem' and 'Solution' above.";
    }

    [RelayCommand]
    private async void OpenCase01Problem()
    {
        await Shell.Current.GoToAsync(nameof(Case01ProblemPage), true);
    }

    [RelayCommand]
    private async void OpenCase01Solution()
    {
        await Shell.Current.GoToAsync(nameof(Case01SolutionPage), true);
    }
}