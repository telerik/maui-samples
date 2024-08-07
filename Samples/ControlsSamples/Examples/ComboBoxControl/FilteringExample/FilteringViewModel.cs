using Microsoft.Maui.Graphics;
using QSF.Examples.DataGridControl.Common;
using QSF.ExampleUtilities;
using QSF.ViewModels;
using System.Collections.ObjectModel;
using Telerik.Maui.Controls;

namespace QSF.Examples.ComboBoxControl.FilteringExample;

public class FilteringViewModel : ExampleViewModel
{
    private bool isFilteringEnabled;
    private SearchMode searchMode;
    private string searchTextPath;
    private string noResultsMessage;
    private string highlightTextColor = "Default";

    public FilteringViewModel()
    {
        this.noResultsMessage = "No person found that matches your search.";
        this.searchTextPath = nameof(SalesPerson.FullName);
        this.People = DataGenerator.GetSalesPersonCollection();
    }

    public bool IsFilteringEnabled
    {
        get => this.isFilteringEnabled;
        set => this.UpdateValue(ref this.isFilteringEnabled, value);
    }

    public SearchMode SearchMode
    {
        get => this.searchMode;
        set => this.UpdateValue(ref this.searchMode, value);
    }

    public string SearchTextPath
    {
        get => this.searchTextPath;
        set => this.UpdateValue(ref this.searchTextPath, value);
    }

    public string NoResultsMessage
    {
        get => this.noResultsMessage;
        set => this.UpdateValue(ref this.noResultsMessage, value);
    }

    public string HighlightTextColor
    {
        get => this.highlightTextColor;
        set => this.UpdateValue(ref this.highlightTextColor, value);
    }

    public ObservableCollection<SalesPerson> People { get; set; }

    public ObservableCollection<string> HighlightTextColors => new ObservableCollection<string>()
    {
        "Default",
        "Black",
        "Red",
        "Green",
        "Blue",
        "Orange"
    };
}
