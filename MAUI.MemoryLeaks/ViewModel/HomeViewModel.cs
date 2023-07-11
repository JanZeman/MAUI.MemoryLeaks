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
        Motivation = "It is year 2023 and we still experience too many memory leaks in MAUI in both released and yet unreleased versions. This project is an attempt to identify some of them.";
    }

    [RelayCommand]
    private async void NavigateToCase01()
    {
        await Shell.Current.GoToAsync(nameof(Case01Page), true);
    }
}