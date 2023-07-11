using MAUI.MemoryLeaks.ViewModel;

namespace MAUI.MemoryLeaks.View;

public partial class HomePage : ContentPage
{
	public HomePage()
	{
		InitializeComponent();
        BindingContext = new HomeViewModel();
    }
}