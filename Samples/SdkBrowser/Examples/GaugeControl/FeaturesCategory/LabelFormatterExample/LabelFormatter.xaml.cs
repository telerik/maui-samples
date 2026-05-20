using Microsoft.Maui.Controls;

namespace SDKBrowserMaui.Examples.GaugeControl.FeaturesCategory.LabelFormatterExample;

public partial class LabelFormatter : ContentView
{
    public LabelFormatter()
    {
        InitializeComponent();

        // >> gauge-label-formatter-csharp
        this.axis.LabelFormatter = value => $"{value}mph";
        // << gauge-label-formatter-csharp
    }
}

