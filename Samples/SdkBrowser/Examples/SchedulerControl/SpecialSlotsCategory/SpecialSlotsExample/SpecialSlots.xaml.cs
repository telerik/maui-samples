using Microsoft.Maui;
using Microsoft.Maui.Controls;
using System;

namespace SDKBrowserMaui.Examples.SchedulerControl.SpecialSlotsCategory.SpecialSlotsExample;

public partial class SpecialSlots : ContentView
{
    public SpecialSlots()
    {
          InitializeComponent();

        // >> scheduler-specialslots-setvm
        this.BindingContext = new ViewModel();
        // << scheduler-specialslots-setvm

        this.scheduler.ScrollIntoView(new TimeOnly(11, 0, 0));
    }
}