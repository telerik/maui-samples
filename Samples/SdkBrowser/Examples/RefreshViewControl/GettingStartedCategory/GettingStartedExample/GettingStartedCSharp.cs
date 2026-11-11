using System.Threading.Tasks;
using Microsoft.Maui.Controls;
using Telerik.Maui.Controls;
using Microsoft.Maui.Graphics;

namespace SDKBrowserMaui.Examples.RefreshViewControl.GettingStartedCategory.GettingStartedExample;

public class GettingStartedCSharp : ContentView
{
    public GettingStartedCSharp()
    {
        // >> refreshview-getting-started-csharp
        var temperatureLabel = new Label { Text = "21 °C" };
        var humidityLabel = new Label { Text = "45 %" };
        var windLabel = new Label { Text = "12 km/h" };
        var refreshCountLabel = new Label { Text = "Not refreshed yet" };
        int refreshCount = 0;

        var refreshView = new RadRefreshView
        {
            Content = new VerticalStackLayout
            {
                Padding = 20,
                Spacing = 12,
                BackgroundColor = Color.FromRgb(238, 238, 238),
                Children =
                {
                    new Label { Text = "Weather Summary", FontSize = 20, FontAttributes = FontAttributes.Bold },
                    new HorizontalStackLayout
                    {
                        Spacing = 8,
                        Children = { new Label { Text = "Temperature:", FontAttributes = FontAttributes.Bold }, temperatureLabel }
                    },
                    new HorizontalStackLayout
                    {
                        Spacing = 8,
                        Children = { new Label { Text = "Humidity:", FontAttributes = FontAttributes.Bold }, humidityLabel }
                    },
                    new HorizontalStackLayout
                    {
                        Spacing = 8,
                        Children = { new Label { Text = "Wind:", FontAttributes = FontAttributes.Bold }, windLabel }
                    },
                    refreshCountLabel
                }
            }
        };

        refreshView.Refreshing += async (s, e) =>
        {
            await Task.Delay(1500);

            refreshCount++;
            temperatureLabel.Text = $"{18 + refreshCount % 10} °C";
            humidityLabel.Text = $"{40 + (refreshCount * 7) % 50} %";
            windLabel.Text = $"{5 + (refreshCount * 3) % 25} km/h";
            refreshCountLabel.Text = $"Refreshed {refreshCount} time(s)";

            refreshView.IsRefreshing = false;
        };
        // << refreshview-getting-started-csharp

        this.Content = refreshView;
    }
}
