using System;
using System.Threading.Tasks;
using Microsoft.Maui.Controls;

namespace SDKBrowserMaui.Examples.RefreshViewControl.ConfigurationCategory.ThresholdsExample;

public partial class Thresholds : ContentView
{
    private int refreshCount;

    public Thresholds()
    {
        InitializeComponent();
    }

    // >> refreshview-thresholds-onrefreshing
    private async void OnRefreshing(object sender, EventArgs e)
    {
        await Task.Delay(1500);
        this.refreshCount++;
        this.statusLabel.Text = $"Refreshed {this.refreshCount} time(s)";
        this.refreshView.IsRefreshing = false;
    }
    // << refreshview-thresholds-onrefreshing
}
