using System;
using System.Collections;
using System.Collections.ObjectModel;
using Telerik.Maui.Controls.Compatibility.Common.Data;

namespace SDKBrowserMaui.Examples.DataGridControl.FilteringCategory;

// >> datagrid-filtering-viewmodel
public class FilteringViewModel
{
    public FilteringViewModel()
    {
        var source = new ObservableCollection<City>();
        source.Add(new City("Vratsa", 54150));
        source.Add(new City("Mexico City", 22000000));
        source.Add(new City("Trieste", 204849));
        source.Add(new City("Ottawa", 934243));
        source.Add(new City("Caracas", 1943901));
        source.Add(new City("Amsterdam", 851573));
        source.Add(new City("Pittsburgh", 305704));
        source.Add(new City("Athens", 664046));
        source.Add(new City("Taipei", 2704974));
        source.Add(new City("Marseille", 855393));
        source.Add(new City("Pnom Penh", 1501725));

        this.GridSource = source;

        this.TextOperatorSource = Enum.GetValues(typeof(TextOperator));
        this.NumericalOperatorSource = Enum.GetValues(typeof(NumericalOperator));
    }

    public ObservableCollection<City> GridSource { get; set; }
    public IList TextOperatorSource { get; set; }
    public IList NumericalOperatorSource { get; set; }
}
// << datagrid-filtering-viewmodel