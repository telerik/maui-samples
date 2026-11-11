using Microsoft.Maui.Controls;

namespace QSF.Examples.ChartsControl.FirstLookExample;

// Displays the default chart palette color for a given series/slice index, so the custom legend
// matches the chart without overriding the series colors.
public class ChartLegendMarker : BoxView
{
    public static readonly BindableProperty PaletteIndexProperty =
        BindableProperty.Create(nameof(PaletteIndex), typeof(int), typeof(ChartLegendMarker), -1, propertyChanged: OnPaletteIndexChanged);

    public int PaletteIndex
    {
        get => (int)this.GetValue(PaletteIndexProperty);
        set => this.SetValue(PaletteIndexProperty, value);
    }

    private static void OnPaletteIndexChanged(BindableObject bindable, object oldValue, object newValue)
    {
        var index = (int)newValue;
        if (index >= 0)
        {
            ((ChartLegendMarker)bindable).SetDynamicResource(ColorProperty, "TelerikChartColor" + ((index % 6) + 1));
        }
    }
}
