using Microsoft.Maui;
using Microsoft.Maui.Controls;
using System;

namespace SDKBrowserMaui.Examples.SchedulerControl.SpecialSlotsCategory.SpecialSlotTemplateExample;

public partial class SpecialSlotTemplate : ContentView
{
	public SpecialSlotTemplate()
	{
		InitializeComponent();

		this.BindingContext = new ViewModel();

		this.scheduler.ScrollIntoView(new TimeOnly(11, 0, 0));
	}
}