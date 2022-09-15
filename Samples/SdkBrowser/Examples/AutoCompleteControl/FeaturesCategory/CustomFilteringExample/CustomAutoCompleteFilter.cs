using SDKBrowserMaui.Examples.AutoCompleteControl.Models;
using Telerik.Maui.Controls;
using Telerik.Maui.Controls.AutoComplete;

namespace SDKBrowserMaui.Examples.AutoCompleteControl.FeaturesCategory.CustomFilteringExample
{
    // >> autocomplete-filtering-class
    public class CustomAutoCompleteFilter : IAutoCompleteFilter
    {
        public bool Filter(object item, string searchText, AutoCompleteCompletionMode completionMode)
        {
            Person person = (Person)item;
            string lowerFirstName = person.FirstName.ToLower();
            string lowerLastName = person.LastName.ToLower();
            string lowerSearchText = searchText.ToLower();
            return lowerFirstName.Contains(lowerSearchText) || lowerLastName.Contains(lowerSearchText);
        }
    }
    // << autocomplete-filtering-class
}
