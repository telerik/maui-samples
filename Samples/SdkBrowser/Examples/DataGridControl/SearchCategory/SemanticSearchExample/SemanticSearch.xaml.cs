using Microsoft.Maui.Controls;
using System;
using System.Collections.Generic;
using Telerik.Maui.Controls.DataGrid;

namespace SDKBrowserMaui.Examples.DataGridControl.SearchCategory.SemanticSearchExample;

public partial class SemanticSearch : ContentView
{
    public SemanticSearch()
    {
        this.InitializeComponent();

        this.BindingContext = new ViewModel();

        // >> datagrid-semanticsearch-provide-search-matches
        // Set up the mock semantic search via the ProvideSearchMatchesAction.
        // In a real scenario, this would call an AI embedding service.
        this.dataGrid.SemanticSearchSettings.ProvideSearchMatchesAction = this.OnProvideSearchMatches;
        // << datagrid-semanticsearch-provide-search-matches
    }

    // >> datagrid-semanticsearch-provide-search-matches-method
    private void OnProvideSearchMatches(DataGridSearchProbe probe)
    {
        if (probe is DataGridSemanticSearchCellProbe cellProbe)
        {
            string cellText = cellProbe.CellValue?.ToString() ?? string.Empty;

            // In a real scenario, this would call an AI embedding service.
            cellProbe.IsMatch = LocalEmbeddingService.IsSemanticMatch(cellProbe.SearchText, cellText);
        }
    }
    // << datagrid-semanticsearch-provide-search-matches-method

    // >> datagrid-semanticsearch-completed
    private void OnSearchCompleted(object sender, EventArgs e)
    {
        // Called when the semantic search operation completes.
    }
    // << datagrid-semanticsearch-completed

    // >> datagrid-semantic-search-starting-event
    private void OnSearchStarting(object sender, DataGridSearchStartingEventArgs e)
    {
        // Called before searching starts.
        // You can modify search terms or cancel the search here.
    }
    // << datagrid-semantic-search-starting-event
}