using MAUI.MemoryLeaks.View;

namespace MAUI.MemoryLeaks;

public partial class AppShell : Shell
{
    public AppShell()
    {
        InitializeComponent();

        Routing.RegisterRoute(nameof(Case01Page), typeof(Case01Page));
    }
}