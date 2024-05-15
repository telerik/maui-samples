using Microsoft.Maui.Controls;

namespace SDKBrowserMaui.Examples.AIPromptControl.FeaturesCategory.SuggestionsExample;

public partial class Suggestions : ContentView
{
	public Suggestions()
	{
		InitializeComponent();

		this.BindingContext = new ViewModel();
	}
}