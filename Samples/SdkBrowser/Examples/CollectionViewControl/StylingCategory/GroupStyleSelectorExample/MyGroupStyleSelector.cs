using Microsoft.Maui.Controls;
using Telerik.Maui.Controls;
using Telerik.Maui.Controls.CollectionView;

namespace SDKBrowserMaui.Examples.CollectionViewControl.StylingCategory.GroupStyleSelectorExample;

// >> collectionview-grouping-styleselector
[ContentProperty(nameof(Styles))]
public class MyGroupStyleSelector : IStyleSelector
{
    public MyGroupStyleSelector()
    {
        this.Styles = new ResourceDictionary();
    }

    public ResourceDictionary Styles { get; }

    public Style SelectStyle(object item, BindableObject bindable)
    {
        if (item is GroupContext group)
        {
            var key = group.Level.ToString();

            if (this.Styles.TryGetValue(key, out var value))
            {
                return (Style)value;
            }
        }

        return null;
    }
}
// << collectionview-grouping-styleselector