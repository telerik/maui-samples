using Microsoft.Maui.Controls;

namespace SDKBrowserMaui.Examples.AutoCompleteControl.StylingCategory.StylingExample;

public partial class Styling : ContentView
{
	public Styling()
	{
		InitializeComponent();

		this.autoComplete.AutomationId = "autoComplete";
	}
}