using Microsoft.Maui.Controls;
using Telerik.Maui.Controls;

namespace SDKBrowserMaui.Examples.SegmentedControl.StylingCategory.StyleSelectorExample;

// >> segmentedcontrol-styleselector
public class BookingStyleSelector : IStyleSelector
{
    public Style BookingStyle { get; set; }

    public Style AllStyle { get; set; }

    public Style SelectStyle(object item, BindableObject bindable)
    {
        var data = item as SegmentItem;
        if (data == null)
        {
            return null;
        }

        return data.Category == "Calendar" ? this.BookingStyle : this.AllStyle;
    }
}
// << segmentedcontrol-styleselector
