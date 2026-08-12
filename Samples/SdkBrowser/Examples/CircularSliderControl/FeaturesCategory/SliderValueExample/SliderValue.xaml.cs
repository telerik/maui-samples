using Microsoft.Maui.Controls;

namespace SDKBrowserMaui.Examples.CircularSliderControl.FeaturesCategory.SliderValueExample;

public partial class SliderValue : ContentView
{
    public SliderValue()
    {
        InitializeComponent();
        this.BindingContext = new ViewModel();
    }
}
