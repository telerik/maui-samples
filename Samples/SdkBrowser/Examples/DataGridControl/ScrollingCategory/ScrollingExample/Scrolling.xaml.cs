using Microsoft.Maui.Controls;
using System.Linq;

namespace SDKBrowserMaui.Examples.DataGridControl.ScrollingCategory.ScrollingExample;

public partial class Scrolling : ContentView
{
	private ViewModel viewModel;

	public Scrolling()
	{
		InitializeComponent();

		this.viewModel = new ViewModel();
		this.BindingContext = this.viewModel;
	}

	// >> datagrid-scrolltoitem
	private void OnScrolltoLastItemClicked(object sender, System.EventArgs e)
	{
		Shop lastItem = this.viewModel.Shops.Last();
		this.dataGrid.ScrollItemIntoView(lastItem);
	}
	// << datagrid-scrolltoitem

	// >> datagrid-scrolltocolumn
	private void OnScrolltoLastColumnClicked(object sender, System.EventArgs e)
	{
		var lastColumn = this.dataGrid.Columns.Last();
		this.dataGrid.ScrollColumnIntoView(lastColumn);
	}
	// << datagrid-scrolltocolumn
}