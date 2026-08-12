using Microsoft.Maui.Controls;
using Telerik.Maui.Controls;

namespace SDKBrowserMaui.Examples.CircularSliderControl.StylingCategory.TicksStyleSelectorExample;

// >> circularslider-ticks-styleselector-class
public class CustomTickStyleSelector : IStyleSelector
{
    public Style MajorTickStyle { get; set; }
    public Style MinorTickStyle { get; set; }

    public Style SelectStyle(object item, BindableObject bindable)
    {
        double value = (double)item;
        return value % 10 == 0 ? this.MajorTickStyle : this.MinorTickStyle;
    }
}
// << circularslider-ticks-styleselector-class
