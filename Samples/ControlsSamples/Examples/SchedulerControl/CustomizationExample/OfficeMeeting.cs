using Telerik.Maui.Controls.Scheduler;

namespace QSF.Examples.SchedulerControl.CustomizationExample;

public class OfficeMeeting : Appointment
{
    public OfficeMeeting()
    {
        this.Location = CustomizationViewModel.DefaultLocations[0];
    }
}
