using Telerik.Maui.Controls;

namespace SDKBrowserMaui.Examples.SlideViewControl.FeaturesCategory.OrientationExample;

public partial class Orientation : RadContentView
{
	public Orientation()
	{
		InitializeComponent();
		
		slideView.BindingContext = new ViewModel();
	}
}