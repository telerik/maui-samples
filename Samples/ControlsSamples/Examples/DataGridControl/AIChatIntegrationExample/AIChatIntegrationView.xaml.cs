using Microsoft.Maui.Controls;
using Telerik.Maui.Controls.DataGrid;

namespace QSF.Examples.DataGridControl.AIChatIntegrationExample;

public partial class AIChatIntegrationView : ContentView
{
    public AIChatIntegrationView()
    {
        this.InitializeComponent();
        this.ConfigureColumns();
    }

    private void ConfigureColumns()
    {
        this.dataGrid.Columns.Add(new DataGridNumericalColumn
        {
            PropertyName = "OrderID",
            HeaderText = "Order ID"
        });

        this.dataGrid.Columns.Add(new DataGridTextColumn
        {
            PropertyName = "ShipName",
            HeaderText = "Ship Name"
        });

        this.dataGrid.Columns.Add(new DataGridTextColumn
        {
            PropertyName = "ShipCity",
            HeaderText = "Ship City"
        });

        this.dataGrid.Columns.Add(new DataGridTextColumn
        {
            PropertyName = "ShipCountry",
            HeaderText = "Ship Country"
        });

        this.dataGrid.Columns.Add(new DataGridDateColumn
        {
            PropertyName = "OrderDate",
            HeaderText = "Order Date"
        });

        this.dataGrid.Columns.Add(new DataGridDateColumn
        {
            PropertyName = "ShippedDate",
            HeaderText = "Shipped Date"
        });

        this.dataGrid.Columns.Add(new DataGridNumericalColumn
        {
            PropertyName = "Freight",
            HeaderText = "Freight",
            CellContentFormat = "${0:N2}"
        });
    }

    private void OnResetChangesButtonClicked(object sender, System.EventArgs e)
    {
        if (this.BindingContext is AIChatIntegrationViewModel viewModel)
        {
            this.dataGrid.ItemsSource = null;
            this.dataGrid.SortDescriptors.Clear();
            this.dataGrid.FilterDescriptors.Clear();
            this.dataGrid.GroupDescriptors.Clear();
            this.dataGrid.SelectedItems.Clear();
            this.dataGrid.Columns.Clear();

            this.ConfigureColumns();
            this.dataGrid.ItemsSource = viewModel.Orders;

            viewModel.IsResetAllowed = false;
        }
    }
}