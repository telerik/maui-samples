using System;
using System.Threading.Tasks;
using Microsoft.Maui.Controls;
using Telerik.Maui.Controls;

namespace SDKBrowserMaui.Examples.RefreshViewControl.StylingCategory.StylingExample;

public partial class Styling : ContentView
{
    public Styling()
    {
        InitializeComponent();
    }

    // >> refreshview-styling-onrefreshing
    private async void OnRefreshing(object sender, EventArgs e)
    {
        await Task.Delay(1000);
        ((RadRefreshView)sender).IsRefreshing = false;
    }
    // << refreshview-styling-onrefreshing
}
