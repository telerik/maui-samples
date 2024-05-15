using Microsoft.Maui.Controls;

namespace SDKBrowserMaui.Examples.ToggleButtonControl.FeaturesCategory.ContentTemplateExample;

public partial class ContentTemplate : ContentView
{
	ViewModel vm;

	public ContentTemplate()
	{
		InitializeComponent();
		this.vm = new ViewModel();
		this.BindingContext = this.vm;
	}
}