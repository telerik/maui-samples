using Microsoft.Maui.Controls.Xaml;
using System.Collections.ObjectModel;
using System.Linq;
using Telerik.Maui.Controls;
using Telerik.Maui.Controls.Compatibility.Common;
using Telerik.Maui.Controls.Compatibility.Common.Data;
using Telerik.Maui.Controls.Compatibility.DataGrid;

namespace SDKBrowserMaui.Examples.DataGridControl.SortingCategory.ProgrammaticSortingExample;

[XamlCompilation(XamlCompilationOptions.Compile)]
public partial class ProgrammaticSorting : RadContentView
{
    private PropertySortDescriptor nameSortDescriptor = new PropertySortDescriptor() { PropertyName = "Name" };
    private PropertySortDescriptor populationSortDescriptor = new PropertySortDescriptor() { PropertyName = "Population" };

    public ProgrammaticSorting()
    {
        this.InitializeComponent();

        var source = new ObservableCollection<Country>();
        source.Add(new Country("Mozambique", 24692000));
        source.Add(new Country("Paraguay", 6725000));
        source.Add(new Country("Turkmenistan", 5663000));
        source.Add(new Country("Mongolia", 3027000));
        source.Add(new Country("Japan", 127000000));
        source.Add(new Country("Bulgaria", 7128000));
        source.Add(new Country("Chad", 14450000));
        source.Add(new Country("Netherlands", 17020000));

        this.BindingContext = source;

        this.columnPicker.ItemsSource = this.grid.Columns.Where(c => c is DataGridTypedColumn).Select(tc => (tc as DataGridTypedColumn).PropertyName).ToList();
        this.columnPicker.SelectedItem = "Name";
    }
            
    private void SortAscButtonClicked(object sender, System.EventArgs e)
    {
        this.grid.SortDescriptors.Clear();
        var descr = this.GetDescriptor();
        descr.SortOrder = SortOrder.Ascending;
        this.grid.SortDescriptors.Add(descr);
    }
    private void SortDescButtonClicked(object sender, System.EventArgs e)
    {
        this.grid.SortDescriptors.Clear();
        var descr = this.GetDescriptor();
        descr.SortOrder = SortOrder.Descending;
        this.grid.SortDescriptors.Add(descr);
    }
    private void ClearSortClicked(object sender, System.EventArgs e)
    {
        this.grid.SortDescriptors.Clear();
    }

    private PropertySortDescriptor GetDescriptor()
    {
        return this.columnPicker.SelectedItem.ToString().Equals("Name") ? this.nameSortDescriptor : this.populationSortDescriptor;
    }
}