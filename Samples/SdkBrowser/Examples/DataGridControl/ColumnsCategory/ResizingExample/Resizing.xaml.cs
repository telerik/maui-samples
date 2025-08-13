using Microsoft.Maui.Controls;
using Telerik.Maui.Controls.DataGrid;
using System;

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
        var formattedWidth = String.Format("{0:N2}", e.Width);
        Application.Current.Windows[0].Page.DisplayAlert("", $"The {(e.Column as DataGridTypedColumn).HeaderText} column width after resizing is {formattedWidth}", "OK");
    }
    // << datagrid-column-resizing-event
}