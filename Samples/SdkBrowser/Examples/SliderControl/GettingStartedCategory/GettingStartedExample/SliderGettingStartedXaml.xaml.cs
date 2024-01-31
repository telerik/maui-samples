using Microsoft.Maui.Controls;

namespace SDKBrowserMaui.Examples.SliderControl.GettingStartedCategory.GettingStartedExample;

public partial class SliderGettingStartedXaml : ContentView
{
	public SliderGettingStartedXaml()
	{
		InitializeComponent();

		slider.AutomationId = "slider";
	}
}