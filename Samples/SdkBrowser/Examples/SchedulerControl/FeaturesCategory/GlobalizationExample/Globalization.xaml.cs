using Microsoft.Maui;
using Microsoft.Maui.Controls;

namespace SDKBrowserMaui.Examples.SchedulerControl.FeaturesCategory.GlobalizationExample;

public partial class Globalization : ContentView
{
	public Globalization()
	{
		InitializeComponent();

        // >> scheduler-culture-set
        this.scheduler.Culture = new System.Globalization.CultureInfo("de-DE");
        // << scheduler-culture-set
    }
}