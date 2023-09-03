using MAUI.MemoryLeaks.View;

namespace MAUI.MemoryLeaks.ViewModel;

public partial class HomeViewModel : BaseViewModel
{
    [ObservableProperty]
    private string _motivation;

    public HomeViewModel()
    {
        PageName = "Leaks";
        Motivation = "It's 2023, and we are still encountering memory leaks in MAUI, both in released and upcoming versions. This project aims to identify some of them.";
        Description =
            "It can be reproduced on Windows. Recently tested in September 2023 (.NET 8.0.100-preview.7)." + Environment.NewLine + Environment.NewLine +
            "The expectation is that after a large number of items are created and then cleared from the given collection, the consumed memory should drop to the original level within a minute or two of starting the experiment. This should happen without having to navigate back to the home screen." + Environment.NewLine + Environment.NewLine +
            " - Start the Windows app" + Environment.NewLine + 
            " - Click the 'ObservableCollection' button and follow up on the page opened." + Environment.NewLine +
            " - Repeat the same with 'Re-assign ObservableCollection'" + Environment.NewLine +
            " - And finally with the 'ObservableList'";
    }

    [RelayCommand]
    private async void NavigateToCase01()
    {
        await Shell.Current.GoToAsync(nameof(Case01Page), true);
    }

    [RelayCommand]
    private async void NavigateToCase01Fix()
    {
        await Shell.Current.GoToAsync(nameof(Case01Workaround01Page), true);
    }
}