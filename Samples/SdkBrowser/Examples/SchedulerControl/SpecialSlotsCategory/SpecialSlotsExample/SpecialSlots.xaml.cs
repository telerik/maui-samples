using Microsoft.Maui;
using Microsoft.Maui.Controls;

namespace SDKBrowserMaui.Examples.SchedulerControl.SpecialSlotsCategory.SpecialSlotsExample;

public partial class SpecialSlots : ContentView
{
	public SpecialSlots()
	{
		InitializeComponent();

        // >> scheduler-specialslots-setvm
        this.BindingContext = new ViewModel();
        // << scheduler-specialslots-setvm
    }
}