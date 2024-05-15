using Microsoft.Maui.Controls;

namespace SDKBrowserMaui.Examples.AIPromptControl.StylingCategory.PopupStylingExample;

public partial class PopupStyling : ContentView
{
	public PopupStyling()
	{
		InitializeComponent();

		this.BindingContext = new AIPromptViewModel();
	}
}