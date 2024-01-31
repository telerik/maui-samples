using Microsoft.Maui.Controls;

namespace SDKBrowserMaui.Examples.DataGridControl.SearchCategory.SearchHighlightingExample;

public partial class SearchHighlighting : ContentView
{
	public SearchHighlighting()
	{
		InitializeComponent();
		this.BindingContext = new ViewModel();
	}
}