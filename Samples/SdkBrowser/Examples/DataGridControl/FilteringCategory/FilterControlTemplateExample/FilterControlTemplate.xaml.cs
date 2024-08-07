using Microsoft.Maui.Controls;

namespace SDKBrowserMaui.Examples.DataGridControl.FilteringCategory.FilterControlTemplateExample;

public partial class FilterControlTemplate : ContentView
{
	public FilterControlTemplate()
	{
		InitializeComponent();

        this.BindingContext = new FilteringViewModel();
    }
}