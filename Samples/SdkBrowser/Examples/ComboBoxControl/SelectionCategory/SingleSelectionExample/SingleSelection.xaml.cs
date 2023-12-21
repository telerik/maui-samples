using Microsoft.Maui.Controls;

namespace SDKBrowserMaui.Examples.ComboBoxControl.SelectionCategory.SingleSelectionExample;

public partial class SingleSelection : ContentView
{
	public SingleSelection()
	{
		InitializeComponent();

		this.BindingContext = new ViewModel();
	}
}