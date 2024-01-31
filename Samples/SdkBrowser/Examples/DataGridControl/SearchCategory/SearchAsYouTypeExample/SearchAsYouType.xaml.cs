using Microsoft.Maui.Controls;

namespace SDKBrowserMaui.Examples.DataGridControl.SearchCategory.SearchAsYouTypeExample;

public partial class SearchAsYouType : ContentView
{
	public SearchAsYouType()
	{
		InitializeComponent();
		this.BindingContext = new ViewModel();
	}
}