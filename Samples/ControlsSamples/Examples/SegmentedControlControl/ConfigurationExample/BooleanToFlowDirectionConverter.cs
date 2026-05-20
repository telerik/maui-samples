using Microsoft.Maui;
using Microsoft.Maui.Controls;
using System.Globalization;

namespace QSF.Examples.SegmentedControlControl.ConfigurationExample;

public class BooleanToFlowDirectionConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        bool checkedState = (bool)value;

        return checkedState ? FlowDirection.RightToLeft : FlowDirection.LeftToRight;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}
