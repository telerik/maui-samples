using Microsoft.Maui.Controls;
using Microsoft.Maui.Controls.Xaml;
using QSF.ViewModels;
using System.ComponentModel;
using Telerik.Maui.Controls;

namespace QSF.Views;

[XamlCompilation(XamlCompilationOptions.Compile)]
public partial class InnerSearchView : RadBorder
{
    public static readonly BindableProperty SearchTextProperty = BindableProperty.Create(
        nameof(SearchText), typeof(string), typeof(InnerSearchView),
        propertyChanged: (b, o, n) => ((InnerSearchView)b).OnSearchTextChanged());

    public static readonly BindableProperty SelectedSearchResultProperty = BindableProperty.Create(
        nameof(SelectedSearchResult), typeof(HighlightedSearchResult), typeof(InnerSearchView), defaultBindingMode: BindingMode.TwoWay);

    public InnerSearchView()
    {
        this.InitializeComponent();

        this.Content.BindingContext = new SearchViewModel();
    }

    public string SearchText
    {
        get { return (string)this.GetValue(SearchTextProperty); }
        set { this.SetValue(SearchTextProperty, value); }
    }

    public HighlightedSearchResult SelectedSearchResult
    {
        get { return (HighlightedSearchResult)this.GetValue(SelectedSearchResultProperty); }
        set { this.SetValue(SelectedSearchResultProperty, value); }
    }

    private void OnSearchTextChanged()
    {
        if (this.Content.BindingContext is SearchViewModel vm)
        {
            vm.SearchText = this.SearchText;
        }
    }

    private void searchResultsListView_PropertyChanged(object sender, PropertyChangedEventArgs e)
    {
        if (this.searchResultsListView != null && e.PropertyName == nameof(this.searchResultsListView.SelectedItem))
        {
            this.SelectedSearchResult = (HighlightedSearchResult)this.searchResultsListView.SelectedItem;
        }
    }
}