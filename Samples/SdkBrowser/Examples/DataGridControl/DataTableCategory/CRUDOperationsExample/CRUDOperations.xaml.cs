using Telerik.Maui.Controls;
using Microsoft.Maui.Controls.Xaml;
using Microsoft.Maui.Controls;
using System;
using Telerik.Maui.Controls.Compatibility.DataGrid;
using System.Linq;

namespace SDKBrowserMaui.Examples.DataGridControl.DataTableCategory.CRUDOperationsExample;

[XamlCompilation(XamlCompilationOptions.Compile)]
public partial class CRUDOperations : RadContentView
{
    DataTableViewModel viewModel;
    public CRUDOperations()
	{
		InitializeComponent();

        this.viewModel = new DataTableViewModel();
        this.BindingContext = viewModel;
    }

    // >> datagrid-datatable-add-row
    private void AddDataClicked(object sender, EventArgs e)
    {
        this.viewModel.Data.Rows.Add(12, "Madrid", 3223000, null, false);
    }
    // << datagrid-datatable-add-row

    // >> datagrid-datatable-update-data
    private void UpdateDataClicked(object sender, EventArgs e)
    {
        if (this.viewModel.Data.Rows.Count > 0)
        {
            this.viewModel.Data.Rows[0]["City"] = "Seoul";
            this.viewModel.Data.Rows[0]["Population"] = 9776000;
        }
    }
    // << datagrid-datatable-update-data

    // >> datagrid-datatable-delete-data
    private void DeleteDataClicked(object sender, EventArgs e)
    {
        int rowCount = this.viewModel.Data.Rows.Count;
        if (rowCount > 0)
        {
            this.viewModel.Data.Rows.RemoveAt(rowCount - 1);
        }
    }
    // << datagrid-datatable-delete-data

    // >> datagrid-datatable-selection
    private void RadDataGrid_SelectionChanged(object sender, DataGridSelectionChangedEventArgs e)
    {
        if (e.AddedItems.Count() > 0)
        {
            var selectedItem = e.AddedItems.First() as DataGridCellInfo;
            Application.Current.MainPage.DisplayAlert("You have selected", $"{selectedItem.Value}", "OK");
        }
    }
    // << datagrid-datatable-selection
}