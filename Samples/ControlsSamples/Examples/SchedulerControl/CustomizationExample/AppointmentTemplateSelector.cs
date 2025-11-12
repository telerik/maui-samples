using Microsoft.Maui.Controls;
using Telerik.Maui.Controls.Scheduler;

namespace QSF.Examples.SchedulerControl.CustomizationExample;

public class AppointmentTemplateSelector : DataTemplateSelector
{
    public DataTemplate DefaultTemplate { get; set; }

    public DataTemplate AgendaTemplate { get; set; }

    protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
    {
        if (item is AgendaAppointmentNode)
        {
            return this.AgendaTemplate;
        }

        return this.DefaultTemplate;
    }
}