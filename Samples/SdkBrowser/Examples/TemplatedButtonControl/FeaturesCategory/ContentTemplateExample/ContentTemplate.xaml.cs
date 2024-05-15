using Microsoft.Maui.Controls;

namespace SDKBrowserMaui.Examples.TemplatedButtonControl.FeaturesCategory.ContentTemplateExample;

public partial class ContentTemplate : ContentView
{
	public ContentTemplate()
	{
		InitializeComponent();

		this.BindingContext = new ViewModel();
	}
}