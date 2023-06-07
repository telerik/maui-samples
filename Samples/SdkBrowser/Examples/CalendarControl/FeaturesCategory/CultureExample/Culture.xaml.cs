using Microsoft.Maui.Controls;
using System.Globalization;

namespace SDKBrowserMaui.Examples.CalendarControl.FeaturesCategory.CultureExample;

public partial class Culture : ContentView
{
	public Culture()
	{
		InitializeComponent();

		// >> calendar-setting-culture
		this.calendar.Culture = new System.Globalization.CultureInfo("ja-JP");
        // << calendar-setting-culture
    }
}