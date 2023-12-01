namespace MAUI.MemoryLeaks.ViewModel;

public partial class HomeViewModel : BaseViewModel
{
    [ObservableProperty]
    private string _motivation;

    public HomeViewModel()
    {
        PageName = "Leaks";
        Motivation = "It is end of 2023, and we are still encountering memory leaks in MAUI. This project is an attempt to target some of them with the hope these will be fixed.";
        Recommendation = "Click the buttons above to navigate to individual cases. These cases are not necessarily isolated, the app restart is recommended.";
        Description =
            "The main target is Microsoft Windows. Ideally please debug together with a feature rich memory profiler tool, e.g. JetBrains DotMemory." +
            $"{Environment.NewLine}{Environment.NewLine}" +
            "Lastly tested in November 2023 (.NET 8.0.1).";

    }

    [RelayCommand]
    private async void OpenCase01()
    {
        await Shell.Current.GoToAsync(nameof(Case01Page), true);
    }

    [RelayCommand]
    private async void OpenCase02()
    {
        await Shell.Current.GoToAsync(nameof(Case02Page), true);
    }
}