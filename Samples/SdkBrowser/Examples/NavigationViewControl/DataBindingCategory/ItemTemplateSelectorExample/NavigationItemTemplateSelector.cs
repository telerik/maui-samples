using Microsoft.Maui.Controls;

namespace SDKBrowserMaui.Examples.NavigationViewControl.DataBindingCategory.ItemTemplateSelectorExample;

// >> navigationview-databinding-templateselector
public class NavigationItemTemplateSelector : DataTemplateSelector
{
    public DataTemplate BadgeTemplate { get; set; }
    public DataTemplate FavoriteTemplate { get; set; }

    protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
    {
        return !string.IsNullOrEmpty(((DataItem)item).Tag) ? BadgeTemplate : FavoriteTemplate;
    }
}
// << navigationview-databinding-templateselector
