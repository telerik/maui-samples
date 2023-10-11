using System;
using System.Globalization;
using Microsoft.Maui.Controls;

namespace SDKBrowserMaui.Examples.RangeSliderControl.TooltipsCategory.TooltipContentTemplateSelectorExample;

// >> rangeslider-tooltips-converter
public class DoubleSubtractConverter : IMultiValueConverter
{
    public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
    {
        double a = values[0] is double v0 ? v0 : 0;
        double b = values[1] is double v1 ? v1 : 0;
        double res = a - b;
        string format = parameter as string;

        if (string.IsNullOrEmpty(format))
        {
            return res;
        }
        else
        {
            string formatted = string.Format(format, res);
            return formatted;
        }
    }

    public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture) => throw new NotImplementedException();
}
// << rangeslider-tooltips-converter