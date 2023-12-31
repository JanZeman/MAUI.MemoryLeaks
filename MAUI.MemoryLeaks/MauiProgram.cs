﻿using MAUI.MemoryLeaks.View;
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
        builder.Services.AddTransient<Case01ProblemViewModel>();
        builder.Services.AddTransient<Case01ProblemPage>();
        builder.Services.AddTransient<Case01SolutionViewModel>();
        builder.Services.AddTransient<Case01SolutionPage>();

        builder.Services.AddTransient<Case02ViewModel>();
        builder.Services.AddTransient<Case02Page>();
        builder.Services.AddTransient<Case02ProblemViewModel>();
        builder.Services.AddTransient<Case02ProblemPage>();
        builder.Services.AddTransient<Case02SolutionViewModel>();
        builder.Services.AddTransient<Case02SolutionPage>();

        var app = builder.Build();
        Services = app.Services;
        return app;
    }
}