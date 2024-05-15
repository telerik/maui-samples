using Microsoft.Maui.Controls;

namespace SDKBrowserMaui.Examples.AIPromptControl.FeaturesCategory.CustomRatingExample;

public partial class CustomResponseRating : ContentView
{
	public CustomResponseRating()
	{
		InitializeComponent();

		this.BindingContext = new AIPromptViewModel();
	}
}