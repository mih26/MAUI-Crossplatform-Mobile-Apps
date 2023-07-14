using R52_Last_Evidence_App.Views;

namespace R52_Last_Evidence_App;

public partial class AppShell : Shell
{
	public AppShell()
	{
		InitializeComponent();
        Routing.RegisterRoute(nameof(DeviceListPage), typeof(DeviceListPage));        
        Routing.RegisterRoute(nameof(AddDevicePage), typeof(AddDevicePage));
        Routing.RegisterRoute(nameof(EditDevicePage), typeof(EditDevicePage));
        Routing.RegisterRoute(nameof(SpecListPage), typeof(SpecListPage));
        Routing.RegisterRoute(nameof(AddSpecPage), typeof(AddSpecPage));
        Routing.RegisterRoute(nameof(EditSpecPage), typeof(EditSpecPage));
        Routing.RegisterRoute(nameof(SpecListPage), typeof(SpecListPage));
    }
}
