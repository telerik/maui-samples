using Microsoft.Maui.Controls;
using SDKBrowserMaui.Examples.DataGridControl.ColumnsCategory;

namespace SDKBrowserMaui.Examples.DataGridControl.ColumnsCategory.CellRendererExample;

public partial class SkiaCellRenderer : ContentView
{
	public SkiaCellRenderer()
	{
		InitializeComponent();

		this.BindingContext = new ViewModel();
	}
}