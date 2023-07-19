using Microsoft.Maui.Controls;

namespace SDKBrowserMaui.Examples.CalendarControl.FeaturesCategory.InteractionModeExample;

public partial class InteractionMode : ContentView
{
	public InteractionMode()
	{
		InitializeComponent();
		this.BindingContext = new ViewModel();
	}
}