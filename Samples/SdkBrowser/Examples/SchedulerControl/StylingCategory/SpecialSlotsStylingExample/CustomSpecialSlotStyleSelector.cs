using Microsoft.Maui.Controls;
using System;
using Telerik.Maui.Controls;
using Telerik.Maui.Controls.Scheduler;

namespace SDKBrowserMaui.Examples.SchedulerControl.StylingCategory.SpecialSlotsStylingExample;

// >> scheduler-specialslots-styleselector
public class CustomSpecialSlotStyleSelector : IStyleSelector
{
    public Style NonWorkingStyle { get; set; }
    public Style WeekendStyle { get; set; }

    public Style SelectStyle(object item, BindableObject bindable)
    {
        var slot = item as SlotNode;
        if (slot.Start.DayOfWeek == DayOfWeek.Friday && slot.End.DayOfWeek == DayOfWeek.Monday)
        {
            return this.WeekendStyle;
        }

        return this.NonWorkingStyle;
    }
}
// << scheduler-specialslots-styleselector