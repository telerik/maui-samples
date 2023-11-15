using Microsoft.Maui.Controls;
using Telerik.Maui.Controls.Scheduler;

namespace SDKBrowserMaui.Examples.SchedulerControl.AppointmentsCategory.AppointmentTemplateExample;

// >> scheduler-customappointmentdatatemplate
public class CustomAppointmentDataTemplate : DataTemplateSelector
{
    public DataTemplate AllDayAppointmentTemplate { get; set; }
    public DataTemplate AppointmentTemplate { get; set; }

    protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
    {
        var appointment = (item as AppointmentNode).Occurrence.Appointment;
        if (appointment.IsAllDay || (appointment.End - appointment.Start).TotalDays > 1)
        {
            return this.AllDayAppointmentTemplate;
        }

        return this.AppointmentTemplate;
    }
}
// << scheduler-customappointmentdatatemplate