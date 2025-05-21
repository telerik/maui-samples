using Microsoft.Maui.Controls;
using Microsoft.Maui.Devices;

namespace SDKBrowserMaui.Examples.CalendarControl.FeaturesCategory.DatePropertiesExample;

public partial class DateProperties : ContentView
{
	public DateProperties()
	{
		InitializeComponent();
		if (DeviceInfo.Platform == DevicePlatform.iOS || DeviceInfo.Platform == DevicePlatform.Android)
		{
			calendar.HorizontalOptions = LayoutOptions.Fill;
			calendar.VerticalOptions = LayoutOptions.Fill;
		}
		else
		{
			calendar.HorizontalOptions = LayoutOptions.Start;
			calendar.VerticalOptions = LayoutOptions.Start;
		}
    }
}