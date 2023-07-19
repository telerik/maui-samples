using System.ComponentModel;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Devices;

namespace QSF.Examples.DataGridControl.RowDetailsExample;

public partial class RowDetailsView : ContentView
{
    public RowDetailsView()
    {
        this.InitializeComponent();

        this.dataGrid.PropertyChanged += this.DataGrid_PropertyChanged;
        this.UpdateRowDetailsTemplate();
    }

    protected override void OnBindingContextChanged()
    {
        base.OnBindingContextChanged();

        var viewModel = this.BindingContext as RowDetailsViewModel;
        if (viewModel != null)
        {
            var orders = viewModel.Orders;
            var expandedRows = this.dataGrid.ExpandedRowDetails;
            expandedRows.Add(orders[0]);
            expandedRows.Add(orders[1]);
            expandedRows.Add(orders[2]);
        }
    }

    private void UpdateRowDetailsTemplate()
    {
        var dictionary = new RowDetailsResources();
        if (this.dataGrid.AreRowDetailsFrozen 
                && (DeviceInfo.Platform == DevicePlatform.Android
                || DeviceInfo.Platform == DevicePlatform.iOS))
        {
            this.dataGrid.RowDetailsTemplate = (DataTemplate)dictionary["CompactOrderDetailsTemplate"];
        }
        else
        {
            this.dataGrid.RowDetailsTemplate = (DataTemplate)dictionary["OrderDetailsTemplate"];
        }
    }

    private void DataGrid_PropertyChanged(object sender, PropertyChangedEventArgs e)
    {
        if (e.PropertyName == nameof(this.dataGrid.AreRowDetailsFrozen))
        {
            this.UpdateRowDetailsTemplate();
        }
    }
}