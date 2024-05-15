using Microsoft.Maui.Controls;

namespace SDKBrowserMaui.Examples.AIPromptControl.StylingCategory.OutputViewStylingExample;

public partial class OutputViewStyling : ContentView
{
	public OutputViewStyling()
	{
		InitializeComponent();

		this.BindingContext = new AIPromptViewModel();
	}
}