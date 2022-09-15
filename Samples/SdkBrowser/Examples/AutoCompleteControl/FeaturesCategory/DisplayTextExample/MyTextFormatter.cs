using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SDKBrowserMaui.Examples.AutoCompleteControl.Models;
using Telerik.Maui.Controls.AutoComplete;

namespace SDKBrowserMaui.Examples.AutoCompleteControl.FeaturesCategory.DisplayTextExample
{
    // >> autocomplete-text-formatter-plain-class
    public class MyTextFormatter : IDisplayTextFormatter
    {
        public string FormatItem(object item)
        {
            var businessItem = item as Client;
            return string.Format("Name: {0}, Email: {1}", businessItem.Name, businessItem.Email);
        }
    }
    // << autocomplete-text-formatter-plain-class
}
