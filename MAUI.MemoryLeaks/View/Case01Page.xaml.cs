using MAUI.MemoryLeaks.ViewModel;

namespace MAUI.MemoryLeaks.View;

public partial class Case01Page
{
	public Case01Page()
	{
		InitializeComponent();
	}

    private void Button_OnClicked(object sender, EventArgs e)
    {
        BindingContext = null;
        BindingContext = new Case01ViewModel();
    }
}