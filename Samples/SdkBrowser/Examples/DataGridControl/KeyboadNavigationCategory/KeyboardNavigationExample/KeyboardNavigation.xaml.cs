using Microsoft.Maui.Controls.Xaml;
using SDKBrowserMaui.Examples.DataGridControl.GroupingCategory.PropertyGroupDescriptorExample;
using Telerik.Maui.Controls;
using Telerik.Maui.Controls.Compatibility.DataGrid;

namespace SDKBrowserMaui.Examples.DataGridControl.KeyboardNavigationCategory.KeyboardNavigationExample;

[XamlCompilation(XamlCompilationOptions.Compile)]
public partial class KeyboardNavigation : RadContentView
{
    public KeyboardNavigation()
    {
        this.InitializeComponent();
        this.BindingContext = new ViewModel();
    }

    // >> datagrid-currentcell-changed
    private void dataGrid_CurrentCellChanged(object sender, CurrentCellChangedEventArgs e)
    {
        var data = e.NewCurrentCell;
        this.cellInfo.Text = data.Value.ToString();
    }
    // << datagrid-currentcell-changed
}