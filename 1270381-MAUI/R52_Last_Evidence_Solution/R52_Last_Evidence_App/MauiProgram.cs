using Microsoft.Extensions.Logging;
using R52_Last_Evidence_App.ViewModels;
using R52_Last_Evidence_App.Views;

namespace R52_Last_Evidence_App;

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
        builder.Services.AddSingleton(typeof(IFilePicker), FilePicker.Default);
        builder.Services.AddSingleton(typeof(DeviceListViewModel));
        builder.Services.AddSingleton(typeof(SpecListViewModel));
        builder.Services.AddSingleton(typeof(AddDevicePage));
        builder.Services.AddSingleton(typeof(EditDevicePage));
        builder.Services.AddSingleton(typeof(DeviceListPage));
        builder.Services.AddSingleton(typeof(SpecListPage));
        builder.Services.AddSingleton(typeof(AddSpecPage));

#if DEBUG
        builder.Logging.AddDebug();
#endif

		return builder.Build();
	}
}
