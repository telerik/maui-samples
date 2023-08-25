using Microsoft.Maui.Controls;
using System;
using System.Globalization;

namespace QSF.Examples.ExpanderControl.FirstLookExample;

public class ViewWidthToSpanCountConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        return (double)value < 641 ? 1 : 2;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        return 2;
    }
}
