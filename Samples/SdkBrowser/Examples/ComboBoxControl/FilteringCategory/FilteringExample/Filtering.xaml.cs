using Microsoft.Maui.Controls;

namespace SDKBrowserMaui.Examples.ComboBoxControl.FilteringCategory.FilteringExample;

public partial class Filtering : ContentView
{
	public Filtering()
	{
		InitializeComponent();

        this.BindingContext = new ViewModel();
    }
}