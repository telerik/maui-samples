using Microsoft.Maui;
using Microsoft.Maui.Controls;
using System;

namespace SDKBrowserMaui.Examples.SchedulerControl.StylingCategory.CurrentTimeIndicatorStylingExample;

public partial class CurrentTimeIndicatorStyling : ContentView
{
	public CurrentTimeIndicatorStyling()
	{
		InitializeComponent();

		this.scheduler.ScrollIntoView(TimeOnly.FromDateTime(DateTime.Now.AddHours(-1)));
	}
}