using Microsoft.Maui.Controls;

namespace SDKBrowserMaui.Examples.AIPromptControl.StylingCategory.TabViewStylingExample;

public partial class TabViewStyling : ContentView
{
	public TabViewStyling()
	{
		InitializeComponent();

		this.BindingContext = new AIPromptViewModel();
	}
}