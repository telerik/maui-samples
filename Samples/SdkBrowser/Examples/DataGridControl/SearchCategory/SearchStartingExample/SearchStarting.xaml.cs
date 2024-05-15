using Microsoft.Maui.Controls;
using System.Collections.Generic;
using System.Linq;
using Telerik.Maui;
using Telerik.Maui.Controls.DataGrid;

namespace SDKBrowserMaui.Examples.DataGridControl.SearchCategory.SearchStartingExample;

public partial class SearchStarting : ContentView
{
	public SearchStarting()
	{
		InitializeComponent();
        this.BindingContext = new ViewModel();
	}

    // >> scheduler-search-searchstarting-event
    private void DataGridSearchSettings_SearchStarting(object sender, DataGridSearchStartingEventArgs args)
    {
        DataGridSearchSettings searchSettings = (DataGridSearchSettings)sender;
        List<string> split = searchSettings.SearchText?.Split(new string[] { " " }, System.StringSplitOptions.RemoveEmptyEntries).ToList();

        args.SearchTerms = split;
        args.SearchTermsLogicalOperator = LogicalOperator.Or;
    }
    // << scheduler-search-searchstarting-event
}