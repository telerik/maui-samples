using Microsoft.Maui.Controls;
using Telerik.Maui.Controls.Calendar;

namespace SDKBrowserMaui.Examples.CalendarControl.TemplatesCategory.TemplateSelectorExample;

// >> calendar-templates-custom-templateselector
public class SpecialDayTemplateSelector : DataTemplateSelector
{
    public DataTemplate NormalDayTemplate { get; set; }

    public DataTemplate SpecialDayTemplate { get; set; }

    protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
    {
        var node = item as CalendarNode;
        var date = node.Date;
        if (date == null)
        {
            return null;
        }

        if (date.Value.Day % 3 == 0 || date.Value.Day % 5 == 0)
        {
            return this.SpecialDayTemplate;
        }

        return this.NormalDayTemplate;
    }
}
// << calendar-templates-custom-templateselector