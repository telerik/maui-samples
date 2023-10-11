using Telerik.Maui.Controls;

namespace SDKBrowserMaui.Examples.RangeSliderControl.FeaturesCategory.RangeThumbExample;

public class ViewModel : NotifyPropertyChangedBase
{
    private double start = 25;
    private double end = 75;

    public double Start
    {
        get { return this.start; }
        set { UpdateValue(ref this.start, value); }
    }

    public double End
    {
        get { return this.end; }
        set { UpdateValue(ref this.end, value); }
    }
}
