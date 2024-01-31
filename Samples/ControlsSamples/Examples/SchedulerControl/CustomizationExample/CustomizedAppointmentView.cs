using Telerik.Maui.Controls.Scheduler;

namespace QSF.Examples.SchedulerControl.CustomizationExample;

public class CustomizedAppointmentView : AppointmentView
{
    protected override void OnHandlerChanged()
    {
        base.OnHandlerChanged();

#if WINDOWS
        var handler = this.Handler;
        if (handler != null)
        {
            var platformAppointment = handler.PlatformView as Telerik.Maui.Platform.MauiAppointmentView;
            if (platformAppointment != null)
            {
                platformAppointment.RecurrenceForeground = new Microsoft.UI.Xaml.Media.SolidColorBrush(Microsoft.UI.Colors.Black);
            }
        }
#endif
    }
}
