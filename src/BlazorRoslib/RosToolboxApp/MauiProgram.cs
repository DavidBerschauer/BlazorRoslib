using Microsoft.Extensions.Logging;
using MudBlazor.Services;
using RosToolboxApp.Data;
using MudBlazor;
using BlazorRoslib.UI;

namespace RosToolboxApp;

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
			});
		builder.Services.AddMauiBlazorWebView();

#if DEBUG
		builder.Services.AddBlazorWebViewDeveloperTools();
		builder.Logging.AddDebug();
#endif
		builder.Services.UseBlazorRoslibUI()
						.AddMudServices(c =>
						{
							c.SnackbarConfiguration.SnackbarVariant = Variant.Filled;
							c.SnackbarConfiguration.PositionClass = Defaults.Classes.Position.TopCenter;
							c.SnackbarConfiguration.ShowCloseIcon = true;
						})
						.AddSingleton<WeatherForecastService>();

		return builder.Build();
	}
}

