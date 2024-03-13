using Microsoft.Maui.Controls;

namespace SDKBrowserMaui.Examples.DataGridControl.GroupingCategory.GroupHeaderTemplateExample;

public partial class CustomGroupHeaderTemplate : ContentView
{
	public CustomGroupHeaderTemplate()
	{
		InitializeComponent();
		
		this.BindingContext = new ViewModel();
	}
}