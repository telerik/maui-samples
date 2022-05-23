using Microsoft.Maui.Controls;
using QSF.ViewModels;

namespace QSF.Selectors
{
    public class SearchResultTemplateSelector : DataTemplateSelector
    {
        public DataTemplate ControlTemplate { get; set; }
        public DataTemplate ControlDescriptionTemplate { get; set; }
        public DataTemplate ExampleTemplate { get; set; }
        public DataTemplate ExampleDescriptionTemplate { get; set; }
        public DataTemplate AllControlsTemplate { get; set; }

        protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
        {
            HighlightedSearchResult hResult = (HighlightedSearchResult)item;

            switch (hResult.ResultType)
            {
                case HighlightedSearchResultType.Control:
                    return this.ControlTemplate;
                case HighlightedSearchResultType.ControlDescription:
                    return this.ControlDescriptionTemplate;
                case HighlightedSearchResultType.Example:
                    return this.ExampleTemplate;
                case HighlightedSearchResultType.ExampleDescription:
                    return this.ExampleDescriptionTemplate;
                case HighlightedSearchResultType.AllControls:
                    return this.AllControlsTemplate;
                default:
                    break;
            }

            return base.SelectTemplate(item, container);
        }
    }
}
