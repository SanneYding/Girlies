using CommunityToolkit.Maui;
using Microsoft.Extensions.Logging;
using vaerkstedGenafl.Models;
using vaerkstedGenafl.ViewModels;
using vaerkstedGenafl.Views;

namespace vaerkstedGenafl
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .UseMauiCommunityToolkit()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

            builder.Services.AddSingleton<OpgavePage>();
            builder.Services.AddSingleton<OpgaveViewModel>();
            
            
            builder.Services.AddTransient<NyOpgaveViewModel>();
            builder.Services.AddTransient<NyOpgavePage>();

            builder.Services.AddTransient<FakturaViewModel>();
            builder.Services.AddTransient<FakturaPage>();

#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
