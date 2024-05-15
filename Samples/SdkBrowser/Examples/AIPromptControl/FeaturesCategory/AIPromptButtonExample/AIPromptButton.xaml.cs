using Microsoft.Maui.Controls;
using SDKBrowserMaui.Examples.AIPromptControl.GettingStartedCategory.GettingStartedExample;

namespace SDKBrowserMaui.Examples.AIPromptControl.FeaturesCategory.AIPromptButtonExample;

public partial class AIPromptButton : ContentView
{
	public AIPromptButton()
	{
		InitializeComponent();

		this.BindingContext = new ViewModel();
	}
}