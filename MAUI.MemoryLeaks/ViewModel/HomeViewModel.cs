using MAUI.MemoryLeaks.View;

namespace MAUI.MemoryLeaks.ViewModel;

public partial class HomeViewModel : BaseViewModel
{
    [ObservableProperty]
    private string _motivation;

    [ObservableProperty]
    private string _description;

    public HomeViewModel()
    {
        Motivation = "It's 2023, and we are still encountering memory leaks in MAUI, both in released and upcoming versions. This project aims to identify some of these issues.";
        Description = "The leak can be reproduced on Windows. It was last tested on September 1st, 2023, using .NET 8.0.100-preview.7." + Environment.NewLine + Environment.NewLine +
                      " - Start the Windows app" + Environment.NewLine + 
                      " - Open the 'Case 1' and observe memory consumed. " + Environment.NewLine + 
                      " - Repeat the same with 'Case 1 Fix'";
    }

    [RelayCommand]
    private async void NavigateToCase01()
    {
        await Shell.Current.GoToAsync(nameof(Case01Page), true);
    }

    [RelayCommand]
    private async void NavigateToCase01Fix()
    {
        await Shell.Current.GoToAsync(nameof(Case01Page), true);
    }
}