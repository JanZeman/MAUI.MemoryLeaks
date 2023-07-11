using MAUI.MemoryLeaks.View;
using MAUI.MemoryLeaks.ViewModel;
using Microsoft.Extensions.Logging;

namespace MAUI.MemoryLeaks;

public static class MauiProgram
{
    public static IServiceProvider Services;

    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            });

#if DEBUG
        builder.Logging.AddDebug();
#endif

        builder.Services.AddSingleton<HomeViewModel>();
        builder.Services.AddSingleton<HomePage>();

        builder.Services.AddTransient<Case01ViewModel>();
        builder.Services.AddTransient<Case01Page>();

        var app = builder.Build();
        Services = app.Services;
        return app;
    }
}