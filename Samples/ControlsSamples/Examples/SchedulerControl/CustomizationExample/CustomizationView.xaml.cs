using Microsoft.Maui.Controls.Xaml;
using System;
using Telerik.Maui.Controls;

namespace QSF.Examples.SchedulerControl.CustomizationExample;

[XamlCompilation(XamlCompilationOptions.Compile)]
public partial class CustomizationView : RadContentView
{
    public CustomizationView()
    {
#if MACCATALYST || WINDOWS
        this.Resources.MergedDictionaries.Add(new CustomSchedulingResources_Desktop());
#else
        this.Resources.MergedDictionaries.Add(new CustomSchedulingResources_Mobile());
#endif

        InitializeComponent();

        this.scheduler.ScrollIntoView(new TimeOnly(12, 0, 0));

        this.scheduler.DialogOpening += (s, e) =>
        {
            if (e.DialogType == Telerik.Maui.Controls.Scheduler.SchedulerDialogType.EditAppointmentDialog)
            {
                this.createAppointmentDialogBtn.IsVisible = false;
                this.officeRoomsGrid.IsVisible = false;
            }
        };

        this.scheduler.DialogClosing += (s, e) =>
        {
            if (e.DialogType == Telerik.Maui.Controls.Scheduler.SchedulerDialogType.EditAppointmentDialog)
            {
                this.createAppointmentDialogBtn.IsVisible = true;
                this.officeRoomsGrid.IsVisible = true;
            }
        };
    }

    private void CreateAppointmentDialogBtnClicked(object sender, System.EventArgs e)
    {
        var now = DateTime.Now;
        this.scheduler.CreateAppointmentWithDialog(new Telerik.Maui.Controls.Scheduler.DateRange(now, now.AddHours(1)));
    }
}
