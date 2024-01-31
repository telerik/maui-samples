using Microsoft.Maui.Controls;
using Telerik.Maui.Controls;

namespace SDKBrowserMaui.Examples.SliderControl.StylingCategory.LabelsStyleSelectorExample;

// >> slider-labels-styleselector-class
public class RangeLabelStyleSelector : IStyleSelector
{
    public Style InsideRangeStyle { get; set; }
    public Style OutsideRangeStyle { get; set; }

    public Style SelectStyle(object item, BindableObject bindable)
    {
        double value = (double)item;
        RadSlider slider = (RadSlider)bindable;
        double originValue = double.IsNaN(slider.OriginValue) ? slider.Minimum : slider.OriginValue;
        bool isInRange = (originValue <= value && value <= slider.Value) || (slider.Value <= value && value <= originValue);
        return isInRange ? this.InsideRangeStyle : this.OutsideRangeStyle;
    }
}
// << slider-labels-styleselector-class