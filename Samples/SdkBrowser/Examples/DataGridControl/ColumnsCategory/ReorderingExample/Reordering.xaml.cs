using Microsoft.Maui.Controls;
using Telerik.Maui.Controls.DataGrid;

namespace SDKBrowserMaui.Examples.DataGridControl.ColumnsCategory.ReorderingExample;

public partial class Reordering : ContentView
{
    public Reordering()
    {
        InitializeComponent();
        this.BindingContext = new ViewModel();
    }

    // >> datagrid-column-reordering-events
    private void OnColumnReorderStarting(object sender, Telerik.Maui.Controls.DataGrid.ColumnReorderStartingEventArgs e)
    {
        if (e.Column.IsFrozen)
        {
            e.Cancel = true;
            Application.Current.MainPage.DisplayAlert("", "Reorder is canceled because the column is Frozen", "OK");
        }
    }

    private void OnColumnReordering(object sender, Telerik.Maui.Controls.DataGrid.ColumnReorderingEventArgs e)
    {
        // add your logic here
    }

    private void OnColumnReorderCompleting(object sender, Telerik.Maui.Controls.DataGrid.ColumnReorderCompletingEventArgs e)
    {
        if (!e.Column.IsFrozen && e.NewIsFrozen)
        {
            e.Cancel = true;
            Application.Current.MainPage.DisplayAlert("", $"Cannot add the {(e.Column as DataGridTypedColumn).HeaderText} column to the frozen area", "OK");
        }
    }

    private void OnColumnReordered(object sender, Telerik.Maui.Controls.DataGrid.ColumnReorderedEventArgs e)
    {
        Application.Current.MainPage.DisplayAlert("", $"{(e.Column as DataGridTypedColumn).HeaderText} column was reordered!", "OK");
    }
    // << datagrid-column-reordering-events
}