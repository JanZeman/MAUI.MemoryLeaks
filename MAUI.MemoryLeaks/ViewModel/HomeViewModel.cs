using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace MAUI.MemoryLeaks.ViewModel;

[ObservableObject]
public partial class HomeViewModel
{
    [ObservableProperty]
    private string _text;

    public HomeViewModel()
    {
        Text = "Binding works :)";
    }

    [RelayCommand]
    private void ChangeText()
    {
        Text = "Command works :)";
    }
}