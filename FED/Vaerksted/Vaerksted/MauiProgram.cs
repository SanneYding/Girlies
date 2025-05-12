using Microsoft.Extensions.Logging;
using Vaerksted.Services;
using Vaerksted.Pages;
using Vaerksted.ViewModels;

namespace Vaerksted;

public static class MauiProgram
{
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

        // Singleton service for database access
        builder.Services.AddSingleton<Database>();

        // ViewModels and Pages registration
        builder.Services.AddTransient<FakturaViewModel>();
        builder.Services.AddTransient<FakturaPage>();

        builder.Services.AddTransient<OpgaveViewModel>();
        builder.Services.AddTransient<Opgaver>();

        builder.Services.AddTransient<KalenderViewModel>(); // Register KalenderViewModel
        builder.Services.AddTransient<KalenderView>();     // Register KalenderView

#if DEBUG
        builder.Logging.AddDebug();
#endif

        return builder.Build();
    }
}
