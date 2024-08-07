using Microsoft.Maui.Controls;
using Telerik.Maui.Controls.DataGrid;

namespace SDKBrowserMaui.Examples.DataGridControl.ColumnsCategory.ResizingExample;

public partial class Resizing : ContentView
{
	public Resizing()
	{
		InitializeComponent();
	}

	// >> datagrid-column-resizing-event
    private void OnDataGridColumnUserResizeCompleted(object sender, Telerik.Maui.Controls.DataGrid.ColumnUserResizeCompletedEventArgs e)
    {
		Application.Current.MainPage.DisplayAlert("", $"The {(e.Column as DataGridTypedColumn).HeaderText} column width after resizing is {e.Width}", "OK");
    }
    // << datagrid-column-resizing-event
}