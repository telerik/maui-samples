using Microsoft.Maui.Controls;

namespace SDKBrowserMaui.Examples.ComboBoxControl.FilteringCategory.CustomFilteringExample;

public partial class CustomFiltering : ContentView
{
	public CustomFiltering()
	{
		InitializeComponent();

        this.BindingContext = new ViewModel();
    }
}