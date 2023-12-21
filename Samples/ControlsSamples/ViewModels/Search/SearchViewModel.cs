using Microsoft.Maui.Controls;
using QSF.Common;
using QSF.Services;
using System;
using System.Collections.Generic;
using System.Linq;

namespace QSF.ViewModels;

public class SearchViewModel : ViewModelBase
{
    private string searchText;
    private IEnumerable<HighlightedSearchResult> searchResults;
    private bool hasSearchResults;
    private HighlightedSearchResult selectedSearchResult;

    public SearchViewModel()
    {
        this.DoSearch();
    }

    public string SearchText
    {
        get
        {
            return searchText;
        }
        set
        {
            if (this.UpdateValue(ref this.searchText, value))
            {
                this.OnSearchTextChanged();
            }
        }
    }

    public IEnumerable<HighlightedSearchResult> SearchResults
    {
        get
        {
            return this.searchResults;
        }
        private set
        {
            if (this.UpdateValue(ref this.searchResults, value))
            {
                this.OnSearchResultsChanged();
            }
        }
    }

    public bool HasSearchResults
    {
        get { return this.hasSearchResults; }
        private set { this.UpdateValue(ref this.hasSearchResults, value); }
    }

    public HighlightedSearchResult SelectedSearchResult
    {
        get { return this.selectedSearchResult; }
        set { this.UpdateValue(ref this.selectedSearchResult, value); }
    }

    private static bool IsNavigateToExampleInstruction(string text)
    {
        return !string.IsNullOrEmpty(text)
            && text.Contains(".")
            && text.StartsWith("#")
            && text.EndsWith("#");
    }

    private static HighlightedSearchResult ToHighlightedSearchResult(SearchResult result)
    {
        HighlightedText hText;
        HighlightedSearchResult hResult;

        switch (result.ResultType)
        {
            case SearchResultType.Control:
                hText = new HighlightedText(result.ControlDisplayName, result.FirstCharIndex, result.LastCharIndex);
                hResult = new HighlightedSearchResult(hText, HighlightedSearchResultType.Control, result.ControlName, result.ControlDisplayName, result.Icon);
                break;
            case SearchResultType.Example:
                hText = new HighlightedText(result.ExampleDisplayName, result.FirstCharIndex, result.LastCharIndex);
                hResult = new HighlightedSearchResult(hText, HighlightedSearchResultType.Example, result.ControlName, result.ControlDisplayName, result.Icon, result.ExampleName, result.ExampleDisplayName);
                break;
            default:
                throw new NotImplementedException();
        }

        return hResult;
    }

    private void OnSearchTextChanged()
    {
        this.DoSearch();
    }

    private void OnSearchResultsChanged()
    {
        this.HasSearchResults = this.SearchResults != null && this.SearchResults.Any();
    }

    private void DoSearch()
    {
        if (IsNavigateToExampleInstruction(this.searchText))
        {
            this.NavigateToExample(this.searchText);
            return;
        }

        List<HighlightedSearchResult> newResults;
        if (string.IsNullOrEmpty(this.searchText))
        {
            newResults = this.GetAllControlsResults();
        }
        else
        {
            newResults = this.GetSearchServiceResults();
        }

        this.SearchResults = newResults;
    }

    private List<HighlightedSearchResult> GetAllControlsResults()
    {
        List<HighlightedSearchResult> newResults = new List<HighlightedSearchResult>();
        IConfigurationService configurationService = DependencyService.Get<IConfigurationService>();
        Configuration configuration = configurationService.Configuration;

        foreach (Control control in configuration.Controls)
        {
            HighlightedText highlightedText = new HighlightedText(control.DisplayName);
            HighlightedSearchResult highlightedSearchResult = new HighlightedSearchResult(highlightedText, HighlightedSearchResultType.AllControls, control.Name, control.DisplayName, control.Icon);
            newResults.Add(highlightedSearchResult);
        }

        return newResults;
    }

    private List<HighlightedSearchResult> GetSearchServiceResults()
    {
        List<HighlightedSearchResult> newResults = new List<HighlightedSearchResult>();
        ISearchService searchService = DependencyService.Get<ISearchService>();
        IEnumerable<SearchResult> searchResults = searchService.Search(this.searchText);

        foreach (SearchResult searchResult in searchResults)
        {
            HighlightedSearchResult highlightedSearchResult = ToHighlightedSearchResult(searchResult);
            newResults.Add(highlightedSearchResult);
        }

        return newResults;
    }

    private bool NavigateToExample(string text)
    {
        if (!IsNavigateToExampleInstruction(text))
        {
            return false;
        }

        string trimmedText = this.searchText.Trim().Substring(1, this.searchText.Length - 2);
        List<string> searchTextFragments = trimmedText.Split(".").ToList<string>();

        if (searchTextFragments.Count == 2)
        {
            string controlName = searchTextFragments[0];
            string exampleName = searchTextFragments[1];
            IConfigurationService configurationService = DependencyService.Get<IConfigurationService>();
            Configuration configuration = configurationService.Configuration;
            Example example = null;

            foreach (Control control in configuration.Controls)
            {
                foreach (Example controlExample in control.Examples)
                {
                    if (controlExample.ControlName == controlName && 
                        controlExample.Name == exampleName)
                    {
                        example = controlExample;
                        break;
                    }
                }
            }

            if (example != null)
            {
                this.SendNavRequest(example);
                return true;
            }
        }
        
        return false;
    }

    private void SendNavRequest(Example example)
    {
        HighlightedText hText = new HighlightedText(example.Name);
        this.SelectedSearchResult = new HighlightedSearchResult(hText, HighlightedSearchResultType.Example, example.ControlName, null, example.Icon);
    }
}
