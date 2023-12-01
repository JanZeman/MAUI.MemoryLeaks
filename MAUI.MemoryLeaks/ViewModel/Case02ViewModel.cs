namespace MAUI.MemoryLeaks.ViewModel;

public partial class Case02ViewModel : BaseViewModel
{
    public Case02ViewModel()
    {
        PageName = "Case 2: Item template leaks";
        Recommendation = "Compare the 'Problem' and 'Solution' above";
        Description =
            "ItemTemplate causes memory leaks. It happens even if there is no any binding used!";
    }

    [RelayCommand]
    private async void OpenCase02Problem()
    {
        await Shell.Current.GoToAsync(nameof(Case02ProblemPage), true);
    }

    [RelayCommand]
    private async void OpenCase02Solution()
    {
        await Shell.Current.GoToAsync(nameof(Case02SolutionPage), true);
    }
}