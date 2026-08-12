using Microsoft.Maui.Controls;
using Telerik.Maui.Controls;

namespace SDKBrowserMaui.Examples.CircularSliderControl.StylingCategory.LabelsStyleSelectorExample;

// >> circularslider-labels-styleselector-class
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
// << circularslider-labels-styleselector-class
