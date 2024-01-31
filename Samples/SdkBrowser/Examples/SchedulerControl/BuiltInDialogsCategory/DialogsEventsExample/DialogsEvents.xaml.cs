using Microsoft.Maui.Controls;

namespace SDKBrowserMaui.Examples.SchedulerControl.BuiltInDialogsCategory.DialogsEventsExample;

public partial class DialogsEvents : ContentView
{
	public DialogsEvents()
	{
		InitializeComponent();
        this.BindingContext = new ViewModel();
	}

    // >> scheduler-dialogs-events
    private void OnDialogOpening(object sender, Telerik.Maui.Controls.Scheduler.SchedulerDialogOpeningEventArgs e)
    {
        if(e.DialogType == Telerik.Maui.Controls.Scheduler.SchedulerDialogType.DeleteRecurrenceChoiceDialog)
        {
            e.Cancel = true;
            logLabel.Text = "Cannot delete the recurrent appointment.";
        }
        else
        {
            logLabel.Text = "Opening ..." + e.DialogType;
        }
    }

    private void OnDialogClosing(object sender, Telerik.Maui.Controls.Scheduler.SchedulerDialogClosingEventArgs e)
    {
        if (!e.DialogResult.HasValue || e.DialogResult.Value == false)
        {
            logLabel.Text = "Closing ..." + e.DialogType;
        }
    }
    // << scheduler-dialogs-events
}