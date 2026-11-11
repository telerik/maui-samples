using System;
using System.Threading.Tasks;
using Microsoft.Maui.Controls;

namespace SDKBrowserMaui.Examples.RefreshViewControl.GettingStartedCategory.GettingStartedExample;

public partial class GettingStartedXaml : ContentView
{
    private int refreshCount;

    public GettingStartedXaml()
    {
        InitializeComponent();
    }

    // >> refreshview-getting-started-refreshing
    private async void OnRefreshing(object sender, EventArgs e)
    {
        await Task.Delay(1500);

        this.refreshCount++;
        this.temperatureLabel.Text = $"{18 + this.refreshCount % 10} °C";
        this.humidityLabel.Text = $"{40 + (this.refreshCount * 7) % 50} %";
        this.windLabel.Text = $"{5 + (this.refreshCount * 3) % 25} km/h";
        this.refreshCountLabel.Text = $"Refreshed {this.refreshCount} time(s)";

        this.refreshView.IsRefreshing = false;
    }
    // << refreshview-getting-started-refreshing
}
