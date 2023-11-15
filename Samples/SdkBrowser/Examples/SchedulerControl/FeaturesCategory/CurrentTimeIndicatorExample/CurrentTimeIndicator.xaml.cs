using Microsoft.Maui;
using Microsoft.Maui.Controls;
using System;
using Telerik.Maui.Controls;

namespace SDKBrowserMaui.Examples.SchedulerControl.FeaturesCategory.CurrentTimeIndicatorExample;

public partial class CurrentTimeIndicator : ContentView
{
	public CurrentTimeIndicator()
	{
		InitializeComponent();

		var now = DateTime.Now;
		var date = now.AddHours(-1);

		TimeOnly time = date.Date != now.Date ? TimeOnly.MinValue : TimeOnly.FromDateTime(date);
		this.scheduler.ScrollIntoView(time);
	}
}