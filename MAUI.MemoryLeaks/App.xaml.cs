using MAUI.MemoryLeaks.ViewModel;

namespace MAUI.MemoryLeaks;

public partial class App : Application
{
    public static Window Window { get; private set; }

    public App()
    {
        InitializeComponent();

        PageAppearing += OnPageAppearing;
        PageDisappearing += OnPageDisappearing;

        MainPage = new AppShell();
    }

    protected override Window CreateWindow(IActivationState activationState)
    {
        Window = base.CreateWindow(activationState);
        return Window;
    }

    private BaseViewModel ResolveViewModel(Page page)
    {
        if (page is not View.MauiPage mauiPage) return null;

        var viewModelType = GetType().Assembly
            .GetTypes().FirstOrDefault(x => x.Name == page.GetType().Name.Replace(nameof(Page), "ViewModel"));

        if (viewModelType == null)
            return null;

        var viewModel = MauiProgram.Services.GetService(viewModelType) as BaseViewModel;
        return viewModel;
    }

    private void OnPageAppearing(object sender, Page page)
    {
        if (page.BindingContext is BaseViewModel viewModel)
        {
            viewModel.OnAppearing();
            return;
        }

        viewModel = ResolveViewModel(page);
        if (viewModel == null) return;

        page.BindingContext = viewModel;
        viewModel.OnAppearing();
    }

    private static void OnPageDisappearing(object sender, Page page)
    {
        if (page.BindingContext is BaseViewModel viewModel)
            viewModel.OnDisappearing();
    }

    public void Dispose()
    {
        PageAppearing -= OnPageAppearing;
        PageDisappearing -= OnPageDisappearing;
    }
}