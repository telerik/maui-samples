using Microsoft.Maui.Controls;
using QSF.ViewModels;

namespace QSF.Selectors;

public class SearchResultTemplateSelector : DataTemplateSelector
{
    public DataTemplate ControlTemplate { get; set; }

    public DataTemplate ExampleTemplate { get; set; }

    protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
    {
        HighlightedSearchResult hResult = (HighlightedSearchResult)item;

        switch (hResult.ResultType)
        {
            case HighlightedSearchResultType.AllControls:
            case HighlightedSearchResultType.Control:
                return this.ControlTemplate;
            case HighlightedSearchResultType.Example:
                return this.ExampleTemplate;
            default:
                break;
        }

        return base.SelectTemplate(item, container);
    }
}
