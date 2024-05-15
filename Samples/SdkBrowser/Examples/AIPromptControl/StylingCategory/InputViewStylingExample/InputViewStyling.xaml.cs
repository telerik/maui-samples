using Microsoft.Maui.Controls;

namespace SDKBrowserMaui.Examples.AIPromptControl.StylingCategory.InputViewStylingExample;

public partial class InputViewStyling : ContentView
{
	public InputViewStyling()
	{
		InitializeComponent();

		this.BindingContext = new AIPromptViewModel();
	}
}