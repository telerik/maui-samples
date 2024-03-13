using Microsoft.Maui.Controls;

namespace SDKBrowserMaui.Examples.DataGridControl.FilteringCategory.CustomFilterTemplateExample;

public partial class CustomFilterTemplate : ContentView
{
	public CustomFilterTemplate()
	{
		InitializeComponent();

		this.BindingContext = new FilteringViewModel();
	}
}