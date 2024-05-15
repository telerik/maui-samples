using Microsoft.Maui.Controls;

namespace SDKBrowserMaui.Examples.AIPromptControl.StylingCategory.SuggestionsStylingExample;

public partial class SuggestionsStyling : ContentView
{
	public SuggestionsStyling()
	{
		InitializeComponent();

		this.BindingContext = new AIPromptViewModel();
	}
}