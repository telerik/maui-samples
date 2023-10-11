using Microsoft.Maui.Controls;
using Telerik.Maui.Controls;

namespace SDKBrowserMaui.Examples.RangeSliderControl.StylingCategory.TicksStyleSelectorExample;

// >> rangeslider-ticks-styleselector-class
public class RangeTickStyleSelector : IStyleSelector
{
    public Style BeforeRangeStyle { get; set; }
    public Style InsideRangeStyle { get; set; }
    public Style AfterRangeStyle { get; set; }

    public Style SelectStyle(object item, BindableObject bindable)
    {
        double value = (double)item;
        RadRangeSlider rangeSlider = (RadRangeSlider)bindable;
        if (value < rangeSlider.RangeStart) return this.BeforeRangeStyle;
        else if (value > rangeSlider.RangeEnd) return this.AfterRangeStyle;
        else return this.InsideRangeStyle;
    }
}
// << rangeslider-ticks-styleselector-class