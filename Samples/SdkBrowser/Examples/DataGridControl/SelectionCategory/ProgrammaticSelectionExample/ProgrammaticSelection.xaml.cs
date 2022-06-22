using Microsoft.Maui.Controls.Xaml;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using Telerik.Maui.Controls;
using Telerik.Maui.Controls.Compatibility.DataGrid;

namespace SDKBrowserMaui.Examples.DataGridControl.SelectionCategory.ProgrammaticSelectionExample;

[XamlCompilation(XamlCompilationOptions.Compile)]
public partial class ProgrammaticSelection : RadContentView
{
    public ProgrammaticSelection()
    {
        this.InitializeComponent();

        // >> datagrid-selection-setvm
        this.BindingContext = new ViewModel();
        // << datagrid-selection-setvm
    }

    private void SelectItemClick(object sender, EventArgs e)
    {
        if(this.dataGrid.SelectionUnit == DataGridSelectionUnit.Row)
        {
            // >> datagrid-selection-selectitem
            var firstMarketingItem = ((ObservableCollection<Person>)this.dataGrid.ItemsSource).First(p => p.Department == "Marketing");
            this.dataGrid.SelectItem(firstMarketingItem);
            // << datagrid-selection-selectitem
        }
        else
        {
            // >> datagrid-selection-selectcell
            var firstMarketingCell = ((ObservableCollection<Person>)this.dataGrid.ItemsSource).First(p => p.Department == "Marketing");
            this.dataGrid.SelectCell(new DataGridCellInfo(firstMarketingCell, this.dataGrid.Columns[2]));
            // << datagrid-selection-selectcell
        }
    }
    
    private void SelectAllClick(object sender, EventArgs e)
    {
        // >> datagrid-selection-selectall
        this.dataGrid.SelectAll();
        // << datagrid-selection-selectall
    }

    private void DeselectAllClick(object sender, EventArgs e)
    {
        this.dataGrid.DeselectAll();           
    }
}