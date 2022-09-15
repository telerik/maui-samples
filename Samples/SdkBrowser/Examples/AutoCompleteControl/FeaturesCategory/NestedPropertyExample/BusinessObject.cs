using SDKBrowserMaui.Examples.AutoCompleteControl.Models;

namespace SDKBrowserMaui.Examples.AutoCompleteControl.FeaturesCategory.NestedPropertyExample
{
    // >> autocomplete-nestedproperty-businessobject
    public class BusinessObject
    {
        public BusinessObject(string name)
        {
            this.Person = new Client() { Name = name };
        }

        public Client Person { get; set; }
    }
    // << autocomplete-nestedproperty-businessobject
}
