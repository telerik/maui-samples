using Microsoft.Maui.Controls;
using Microsoft.Maui.Graphics;
using System;
using System.Globalization;

namespace QSF.Examples.CalendarControl.CustomizationExample;

public class ColorWithAlphaConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is Color color && parameter != null)
        {
            return color.WithAlpha(float.Parse(parameter.ToString()));
        }

        return value;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => value;
}