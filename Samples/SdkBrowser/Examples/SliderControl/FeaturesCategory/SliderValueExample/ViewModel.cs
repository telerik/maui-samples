using Telerik.Maui.Controls;

namespace SDKBrowserMaui.Examples.SliderControl.FeaturesCategory.SliderValueExample;

public class ViewModel : NotifyPropertyChangedBase
{
    private double myValue = 35;

    public double MyValue
    {
        get { return this.myValue; }
        set { UpdateValue(ref this.myValue, value); }
    }
}