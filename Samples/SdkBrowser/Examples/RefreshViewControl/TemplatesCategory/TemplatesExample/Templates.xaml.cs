using System;
using System.Threading.Tasks;
using Microsoft.Maui.Controls;

namespace SDKBrowserMaui.Examples.RefreshViewControl.TemplatesCategory.TemplatesExample;

public partial class Templates : ContentView
{
    private int refreshCount;

    public Templates()
    {
        InitializeComponent();
    }

    // >> refreshview-templates-onrefreshing

    private async void OnRefreshing(object sender, EventArgs e)
    {
        await Task.Delay(2000);
        this.refreshCount++;
        this.statusLabel.Text = $"Refreshed {this.refreshCount} time(s)";
        this.refreshView.IsRefreshing = false;
    }
    // << refreshview-templates-onrefreshing
}
