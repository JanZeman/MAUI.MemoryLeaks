namespace MAUI.MemoryLeaks;

public partial class AppShell : Shell
{
    public AppShell()
    {
        InitializeComponent();

        Routing.RegisterRoute(nameof(Case01Page), typeof(Case01Page));
        Routing.RegisterRoute(nameof(Case01ProblemPage), typeof(Case01ProblemPage));
        Routing.RegisterRoute(nameof(Case01SolutionPage), typeof(Case01SolutionPage));

        Routing.RegisterRoute(nameof(Case02Page), typeof(Case02Page));
        Routing.RegisterRoute(nameof(Case02ProblemPage), typeof(Case02ProblemPage));
        Routing.RegisterRoute(nameof(Case02SolutionPage), typeof(Case02SolutionPage));
    }
}