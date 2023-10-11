using Microsoft.Maui.Controls;
using System;
using Telerik.Maui.Controls;
using Telerik.Maui.Controls.Scheduler;

namespace SDKBrowserMaui.Examples.SchedulerControl.StylingCategory.AppointmentStylingExample;

// >> scheduler-customappointmentstyleselector
public class CustomAppointmentStyleSelector : IStyleSelector
{
    public Style PastAppointmentStyle { get; set; }
    public Style FutureAppointmentStyle { get; set; }

    public Style SelectStyle(object item, BindableObject bindable)
    {
        var appointment = (item as AppointmentNode).Occurrence;
        if (appointment.Start < DateTime.Now)
        {
            return this.PastAppointmentStyle;
        }

        return this.FutureAppointmentStyle;
    }
}
// << scheduler-customappointmentstyleselector