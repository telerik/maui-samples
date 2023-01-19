using Microsoft.Maui.Controls;
using Telerik.Maui.Controls.Compatibility.DataGrid;

namespace QSF.Examples.DataGridControl.FrozenColumnsExample;

public partial class ConfigurationArea : ContentView
{
    public ConfigurationArea()
    {
        this.InitializeComponent();
    }

    private void OnItemTapped(object sender, object e)
    {
        var column = ((View)sender).BindingContext as DataGridColumn;
        if (column != null)
        {
            column.IsFrozen = !column.IsFrozen;
        }
    }
}