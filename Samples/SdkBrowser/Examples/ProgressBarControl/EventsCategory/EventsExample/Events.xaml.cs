using Microsoft.Maui.Controls;
using System;
using Telerik.Maui.Controls;

namespace SDKBrowserMaui.Examples.ProgressBarControl.EventsCategory.EventsExample;

public partial class Events : ContentView
{
    public Events()
    {
        InitializeComponent();
    }

    // >> progressbar-progresschanged-event
    private void OnProgressChanged(object sender, ProgressChangedEventArgs e)
    {
        this.statusLabel.Text = e.Progress.ToString();
    }
    // << progressbar-progresschanged-event

    // >> progressbar-progresscompleted-event
    private void OnProgressCompleted(object sender, EventArgs e)
    {
        this.statusLabel.Text = "Completed";
    }
    // << progressbar-progresscompleted-event
}
