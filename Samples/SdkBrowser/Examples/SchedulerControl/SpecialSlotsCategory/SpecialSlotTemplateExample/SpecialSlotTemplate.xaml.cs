using Microsoft.Maui;
using Microsoft.Maui.Controls;

namespace SDKBrowserMaui.Examples.SchedulerControl.SpecialSlotsCategory.SpecialSlotTemplateExample;

public partial class SpecialSlotTemplate : ContentView
{
	public SpecialSlotTemplate()
	{
		InitializeComponent();

		this.BindingContext = new ViewModel();
	}
}