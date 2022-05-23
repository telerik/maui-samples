using Microsoft.Maui.Controls.Xaml;
using System.Collections.ObjectModel;
using Telerik.Maui.Controls;

namespace SDKBrowserMaui.Examples.DataGridControl.StylingCategory.ColumnsStylingExample;

[XamlCompilation(XamlCompilationOptions.Compile)]
public partial class ColumnsStyling : RadContentView
{
    public ColumnsStyling ()
    {
        InitializeComponent ();

        var source = new ObservableCollection<Data>();
        source.Add(new Data("India", "New Delhi"));
        source.Add(new Data("South Africa", "Cape Town"));
        source.Add(new Data("Nigeria", "Abuja"));
        source.Add(new Data("Singapore", "Singapore"));

        this.BindingContext = source;
    }
}