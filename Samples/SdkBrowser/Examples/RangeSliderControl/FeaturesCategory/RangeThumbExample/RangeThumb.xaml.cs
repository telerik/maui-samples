using Microsoft.Maui.Controls;

namespace SDKBrowserMaui.Examples.RangeSliderControl.FeaturesCategory.RangeThumbExample;

public partial class RangeThumb : ContentView
{
	public RangeThumb()
	{
		InitializeComponent();
		this.BindingContext = new ViewModel();
	}
}