using Microsoft.Maui.Controls;

namespace SDKBrowserMaui.Examples.AIPromptControl.StylingCategory.AIPromptButtonStylingExample;

public partial class AIPromptButtonStyling : ContentView
{
	public AIPromptButtonStyling()
	{
		InitializeComponent();

		this.BindingContext = new AIPromptViewModel();
	}
}