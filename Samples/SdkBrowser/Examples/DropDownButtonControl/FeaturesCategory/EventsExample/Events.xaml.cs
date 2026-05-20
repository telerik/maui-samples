using System.ComponentModel;
using Microsoft.Maui.Controls;

namespace SDKBrowserMaui.Examples.DropDownButtonControl.FeaturesCategory.EventsExample;

public partial class Events : ContentView
{
    public Events()
    {
        this.InitializeComponent();
    }

    // >> dropdownbutton-events-handlers
    private void OnDropDownOpening(object sender, CancelEventArgs e)
    {
        if (this.cancelOpeningCheckBox?.IsChecked == true)
        {
            e.Cancel = true;
            this.AppendLog("Opening was canceled.");
            return;
        }

        this.AppendLog("Opening");
    }

    private void OnDropDownOpened(object sender, System.EventArgs e)
    {
        this.AppendLog("Opened");
    }

    private void OnDropDownClosed(object sender, System.EventArgs e)
    {
        this.AppendLog("Closed");
    }

    private void AppendLog(string message)
    {
        this.eventsLog.Text = string.IsNullOrEmpty(this.eventsLog.Text)
            ? message
            : $"{this.eventsLog.Text}\n{message}";
    }
    // << dropdownbutton-events-handlers
}
