using Microsoft.Maui.Controls;
using Telerik.Maui;

namespace SDKBrowserMaui.Examples.CircularSliderControl.EventsCategory.ValueChangingExample;

public partial class ValueChanging : ContentView
{
    public ValueChanging()
    {
        InitializeComponent();
    }

    // >> circularslider-valuechanging-csharp
    private void OnValueChanging(object sender, ValueChangingEventArgs e)
    {
        // Snap the value to the nearest multiple of 5 while dragging.
        e.Value = System.Math.Round(e.Value / 5) * 5;

        this.valueLabel.Text = $"Value: {e.Value:N0}";
    }
    // << circularslider-valuechanging-csharp
}
