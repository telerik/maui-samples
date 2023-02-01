using Microsoft.Maui.Controls;

namespace QSF.Examples.DataGridControl.FrozenColumnsExample;

public partial class FrozenColumnsView : ContentView
{
    public FrozenColumnsView()
    {
        this.InitializeComponent();
    }

    protected override void OnBindingContextChanged()
    {
        base.OnBindingContextChanged();

        var viewModel = this.BindingContext as FrozenColumnsViewModel;
        if (viewModel != null)
        {
            viewModel.Columns = this.dataGrid.Columns;
        }
    }
}