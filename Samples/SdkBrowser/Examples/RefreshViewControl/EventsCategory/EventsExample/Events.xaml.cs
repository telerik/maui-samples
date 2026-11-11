using System;
using System.Threading.Tasks;
using Microsoft.Maui.Controls;
using Telerik.Maui.Controls.RefreshView;

namespace SDKBrowserMaui.Examples.RefreshViewControl.EventsCategory.EventsExample;

public partial class Events : ContentView
{
    public Events()
    {
        InitializeComponent();
    }

    // >> refreshview-events-handlers
    private void OnPulling(object sender, RefreshViewPullingEventArgs e)
    {
        this.progressLabel.Text = $"Progress: {e.Progress:P0}";

        if (this.cancelCheckBox.IsChecked == true && e.Progress > 0.5)
        {
            e.Cancel = true;
            this.Log("Pulling canceled");
        }
    }

    private async void OnRefreshing(object sender, EventArgs e)
    {
        this.Log("Refreshing");
        await Task.Delay(1500);
        this.refreshView.IsRefreshing = false;
    }

    private void OnRefreshed(object sender, EventArgs e)
    {
        this.Log("Refreshed");
    }
    // << refreshview-events-handlers

    private void Log(string message)
    {
        this.logLabel.Text = message + Environment.NewLine + this.logLabel.Text;
    }
}
