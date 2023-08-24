using Telerik.Maui.Controls;

namespace SDKBrowserMaui.Examples.SlideViewControl.StylingCategory.IndicatorStylingExample;

public partial class IndicatorStyling : RadContentView
{
	public IndicatorStyling()
	{
		InitializeComponent();

		this.slideView.BindingContext = new ViewModel();
	}
}