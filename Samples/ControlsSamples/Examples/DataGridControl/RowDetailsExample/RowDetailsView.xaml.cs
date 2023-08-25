using System.ComponentModel;
using Microsoft.Maui.Controls;

namespace QSF.Examples.DataGridControl.RowDetailsExample;

public partial class RowDetailsView : ContentView
{
    public RowDetailsView()
    {
        this.InitializeComponent();
    }

    protected override void OnBindingContextChanged()
    {
        base.OnBindingContextChanged();

        if (this.BindingContext is RowDetailsViewModel viewModel)
        {
            this.dataGrid.ExpandedRowDetails.Add(viewModel.Employees[0]);
        }
    }

    private void DataGrid_PropertyChanged(object sender, PropertyChangedEventArgs e)
    {
        if (e.PropertyName == nameof(this.dataGrid.AreRowDetailsFrozen))
        {
            var dictionary = new RowDetailsResources();
            this.dataGrid.RowDetailsTemplate = (DataTemplate)dictionary["DataGridRowDetailsTemplate"];
        }
    }
}