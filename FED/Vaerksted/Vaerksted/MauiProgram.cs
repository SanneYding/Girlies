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

		builder.Services.AddSingleton<Database>();
		builder.Services.AddTransient<FakturaViewModel>();
		builder.Services.AddTransient<FakturaPage>();
		builder.Services.AddTransient<OpgaveViewModel>();
		builder.Services.AddTransient<Opgaver>();

#if DEBUG
		builder.Logging.AddDebug();
#endif

		return builder.Build();
	}
}
