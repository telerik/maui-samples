using Microsoft.Maui.Controls;
using System.Collections.Generic;

namespace SDKBrowserMaui.Examples.SegmentedControl.DataBindingCategory;

// >> segmentedcontrol-datatemplateselector
public class EvenOddDataTemplateSelector : DataTemplateSelector
{
    public DataTemplate CalendarTemplate { get; set; }

    public DataTemplate CommonTemplate { get; set; }

    protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
    {
        var data = item as SegmentItem;
        if(data == null)
        {
            return null;
        }

        return data.Category == "Calendar" ? this.CalendarTemplate : this.CommonTemplate;
    }
}
// << segmentedcontrol-datatemplateselector
