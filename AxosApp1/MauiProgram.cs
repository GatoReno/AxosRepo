using Microsoft.Extensions.Logging;
using AxosApp1.ViewModels;
using AxosApp1.Abstractions;
namespace AxosApp1;

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

#if DEBUG
		builder.Logging.AddDebug();
#endif
		builder.Services.AddTransient<MainPage>();
    	builder.Services.AddTransient<MainViewModel>();
		DependencyService.RegisterSingleton<IAppInfoService>(new AppInfoService());
		return builder.Build();
	}
}
