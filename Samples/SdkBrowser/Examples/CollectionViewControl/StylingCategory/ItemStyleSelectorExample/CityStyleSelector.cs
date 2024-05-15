using Microsoft.Maui.Controls;
using Telerik.Maui.Controls;

namespace SDKBrowserMaui.Examples.CollectionViewControl.StylingCategory.ItemStyleSelectorExample;

// >> collectionview-styleselector
public class CityStyleSelector : IStyleSelector
{
    public Style OddStyle { get; set; }

    public Style EvenStyle { get; set; }

    public Style SelectStyle(object item, BindableObject bindable)
    {
        var data = (item as DataModel);
        if (data.Id % 2 == 0)
        {
            return EvenStyle;
        }
        else return OddStyle;
    }
}
// << collectionview-styleselector
