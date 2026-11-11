using System;
using Microsoft.Maui.Controls;

namespace SDKBrowserMaui.Examples.RefreshViewControl.ConfigurationCategory.RefreshStateExample;

public partial class RefreshState : ContentView
{
    private int refreshCount;

    public RefreshState()
    {
        InitializeComponent();
    }

    // >> refreshview-refresh-state-programmatic
    private void OnStartRefreshClicked(object sender, EventArgs e)
    {
        this.refreshView.IsRefreshing = true;
    }

    private void OnStopRefreshClicked(object sender, EventArgs e)
    {
        this.refreshView.IsRefreshing = false;
    }
    // << refreshview-refresh-state-programmatic

    private void OnRefreshed(object sender, EventArgs e)
    {
        this.refreshCount++;
        this.statusLabel.Text = $"Refresh completed {this.refreshCount} time(s)";
    }
}
