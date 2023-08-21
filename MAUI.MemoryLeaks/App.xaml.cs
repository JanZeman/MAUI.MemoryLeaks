using MAUI.MemoryLeaks.ViewModel;

namespace MAUI.MemoryLeaks;

public partial class App : Application
{
    public static Window Window { get; private set; }

    public App()
    {
        InitializeComponent();

        PageAppearing += Appearing;
        
        PageDisappearing += Disappearing;

        MainPage = new AppShell();
    }

    protected override Window CreateWindow(IActivationState activationState)
    {
        Window = base.CreateWindow(activationState);
        return Window;
    }

    private BaseViewModel ResolveViewModel(Page page)
    {
        var viewModelType = GetType().Assembly.GetTypes().Where(x => x.Name == page.GetType().Name.Replace(nameof(Page), "ViewModel")).ToList();

        if (!viewModelType.Any())
            return null;

        var resolvedViewModel = MauiProgram.Services.GetService(viewModelType.First()) as BaseViewModel;
        return resolvedViewModel;
    }

    private void Appearing(object sender, Page page)
    {
        if (!page.GetType().Name.Contains(nameof(Page)))
            return;

        if (page.BindingContext is not BaseViewModel viewModel)
        {
            viewModel = ResolveViewModel(page);
            page.BindingContext = viewModel;
        }

        viewModel.OnAppearing();
    }

    private void Disappearing(object sender, Page page)
    {
        if (page.BindingContext is BaseViewModel viewModel)
            viewModel.OnDisappearing();
    }

    public void Dispose()
    {
        PageAppearing -= Appearing;
        PageDisappearing -= Disappearing;
    }
}