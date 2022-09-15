using System.Collections.Generic;

namespace SDKBrowserMaui.Examples.AutoCompleteControl.FeaturesCategory.NestedPropertyExample
{
    // >> autocomplete-nestedproperty-viewmodel
    public class NestedPropertyViewModel
    {
        public NestedPropertyViewModel()
        {
            this.Source = new List<BusinessObject>()
            {
                new BusinessObject("Freda Curtis"),
                new BusinessObject("Jeffery Francis"),
                new BusinessObject("Eva Lawson"),
                new BusinessObject("Emmett Santos"),
                new BusinessObject("Theresa Bryan"),
                new BusinessObject("Terrell Norris"),
                new BusinessObject("Eric Wheeler"),
                new BusinessObject("Julius Clayton"),
                new BusinessObject("Alfredo Thornton"),
                new BusinessObject("Roberto Romero"),
                new BusinessObject("Orlando Mathis"),
                new BusinessObject("Eduardo Thomas"),
                new BusinessObject("Harry Douglas"),
                new BusinessObject("Parker Blanton"),
                new BusinessObject("Leanne Motton"),
                new BusinessObject("Shanti Osborn"),
                new BusinessObject("Merry Lasker"),
                new BusinessObject("Kizzie Arjona"),
                new BusinessObject("Augusta Hentz"),
                new BusinessObject("Hong Telesco"),
                new BusinessObject("Inez Landi"),
                new BusinessObject("Shantel Jarrell")
            };
        }

        public List<BusinessObject> Source { get; set; }
    }
    // << autocomplete-nestedproperty-viewmodel
}
