using Microsoft.Maui;
using Microsoft.Maui.Controls;

namespace SDKBrowserMaui.Examples.SchedulerControl.StylingCategory.SpecialSlotsStylingExample;

public partial class SpecialSlotsStyling : ContentView
{
	public SpecialSlotsStyling()
	{
		InitializeComponent();
		this.BindingContext = new ViewModel();
	}
}