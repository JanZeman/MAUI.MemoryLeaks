using MAUI.MemoryLeaks.View;

namespace MAUI.MemoryLeaks.ViewModel;

public partial class HomeViewModel : BaseViewModel
{
    [ObservableProperty]
    private string _motivation;

    [ObservableProperty]
    private string _text;

    public HomeViewModel()
    {
        Motivation = "It's 2023, and we are still encountering numerous memory leaks in MAUI, both in released and upcoming versions. This project aims to identify some of these issues.";
    }

    [RelayCommand]
    private async void NavigateToCase01()
    {
        await Shell.Current.GoToAsync(nameof(Case01Page), true);
    }
}