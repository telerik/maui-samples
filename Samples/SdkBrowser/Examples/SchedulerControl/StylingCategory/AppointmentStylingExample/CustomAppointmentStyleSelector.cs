using Microsoft.Maui.Controls;
using System;
using Telerik.Maui.Controls;
using Telerik.Maui.Controls.Scheduler;

namespace SDKBrowserMaui.Examples.SchedulerControl.StylingCategory.AppointmentStylingExample;

// >> scheduler-customappointmentstyleselector
public class CustomAppointmentStyleSelector : IStyleSelector
{
    public Style AllDayAppointmentStyle { get; set; }
    public Style ExpiredAppointmentStyle { get; set; }
    public Style OngoingAppointmentStyle { get; set; }

    public Style SelectStyle(object item, BindableObject bindable)
    {
        var appointment = (item as AppointmentNode).Occurrence.Appointment;

        if (appointment.IsAllDay)
        {
            return this.AllDayAppointmentStyle;
        }
        else if (appointment.Start < DateTime.Now)
        {
            return this.ExpiredAppointmentStyle;
        }
        else
        {
            return this.OngoingAppointmentStyle;
        }
    }
}
// << scheduler-customappointmentstyleselector