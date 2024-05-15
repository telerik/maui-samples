using Microsoft.Maui.Controls;

namespace SDKBrowserMaui.Examples.AIPromptControl.StylingCategory.CommandsStylingExample;

public partial class CommandsViewStyling : ContentView
{
	public CommandsViewStyling()
	{
		InitializeComponent();

		this.BindingContext = new AIPromptViewModel();
	}
}