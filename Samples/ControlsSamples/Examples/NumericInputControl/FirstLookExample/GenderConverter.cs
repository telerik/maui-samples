using Microsoft.Maui.Controls;
using System;
using System.Globalization;

namespace QSF.Examples.NumericInputControl.FirstLookExample;

public class GenderConverter : IValueConverter
{
    public object Convert(object value, Type type, object parameter, CultureInfo culture)
    {
        return (int)(Gender)value;
    }

    public object ConvertBack(object value, Type type, object parameter, CultureInfo culture)
    {
        return (Gender)(int)value;
    }
}
