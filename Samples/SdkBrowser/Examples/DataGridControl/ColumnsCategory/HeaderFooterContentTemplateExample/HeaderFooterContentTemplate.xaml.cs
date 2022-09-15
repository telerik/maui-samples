using Microsoft.Maui.Controls;
using System.Collections.Generic;
using Telerik.Maui.Controls;

namespace SDKBrowserMaui.Examples.DataGridControl.ColumnsCategory.HeaderFooterContentTemplateExample;

public partial class HeaderFooterContentTemplate : RadContentView
{
    public HeaderFooterContentTemplate()
    {
        InitializeComponent();

        this.dataGrid.ItemsSource = new List<Data>
        {
            new Data { Country = "India", Capital = "New Delhi"},
            new Data { Country = "South Africa", Capital = "Cape Town"},
            new Data { Country = "Nigeria", Capital = "Abuja" },
            new Data { Country = "Singapore", Capital = "Singapore" }
        };
    }
}