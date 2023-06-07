using Telerik.Maui.Controls;

namespace QSF.Examples.DataGridControl.EmptyTemplateExample;

public partial class EmptyTemplate : RadContentView
{
    public EmptyTemplate()
    {
        InitializeComponent();
    }

    protected override void OnBindingContextChanged()
    {
        base.OnBindingContextChanged();

        var viewModel = this.BindingContext as EmptyTemplateViewModel;
        if (viewModel != null)
        {
            viewModel.Columns = this.dataGrid.Columns;
        }
    }
}