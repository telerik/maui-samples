using Microsoft.Maui.Controls.Xaml;
using System.Collections.Generic;
using Telerik.Maui.Controls;

namespace SDKBrowserMaui.Examples.DataGridControl.StylingCategory.DataGridStylingExample;

[XamlCompilation(XamlCompilationOptions.Compile)]
public partial class DataGridStyling : RadContentView
{
    public DataGridStyling ()
    {
        InitializeComponent ();

        this.DataGrid.ItemsSource = new List<Data>
        {
            new Data { Country = "India", Capital = "New Delhi"},
            new Data { Country = "South Africa", Capital = "Cape Town"},
            new Data { Country = "Nigeria", Capital = "Abuja" },
            new Data { Country = "Singapore", Capital = "Singapore" }
        };
    }
}