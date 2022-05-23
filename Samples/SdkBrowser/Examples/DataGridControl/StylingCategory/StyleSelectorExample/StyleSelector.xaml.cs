using Microsoft.Maui.Controls.Xaml;
using System.Collections.ObjectModel;
using Telerik.Maui.Controls;

namespace SDKBrowserMaui.Examples.DataGridControl.StylingCategory.StyleSelectorExample;

[XamlCompilation(XamlCompilationOptions.Compile)]
public partial class StyleSelector : RadContentView
{
    public StyleSelector ()
    {
        InitializeComponent ();
        // >> datagrid-styleselector-items
        var source = new ObservableCollection<Data>();
        source.Add(new Data("India", "New Delhi"));
        source.Add(new Data("South Africa", "Cape Town"));
        source.Add(new Data("Nigeria", "Abuja"));
        source.Add(new Data("Singapore", "Singapore"));
        source.Add(new Data("Romania", "Bucharest"));
        source.Add(new Data("Spain", "Madrid"));
        source.Add(new Data("France", "Paris"));
        source.Add(new Data("Japan", "Tokyo"));

        this.datagrid.ItemsSource = source;
        // << datagrid-styleselector-items
    }
}