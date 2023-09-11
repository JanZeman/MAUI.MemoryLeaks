using System.Diagnostics;
using MAUI.MemoryLeaks.ViewModel;

namespace MAUI.MemoryLeaks.View;

public class MauiPage : ContentPage
{
    protected BaseViewModel ViewModel { get; private set; }

    protected MauiPage()
    {
        Loaded += OnLoaded;
    }

    protected override void OnBindingContextChanged()
    {
        base.OnBindingContextChanged();

        if (BindingContext is BaseViewModel viewModel)
            ViewModel = viewModel;
    }

    private void OnWindowActivated(object sender, EventArgs e)
    {
        Debug.WriteLine("OnWindowActivated");
    }

    private void OnLoaded(object sender, EventArgs e)
    {
        Debug.WriteLine("OnLoaded");
        ViewModel?.OnLoaded();
    }
}