using Microsoft.Maui.Controls;
using System;
using System.Globalization;
using Telerik.Maui;

namespace QSF.Examples.DockLayoutControl.FirstLookExample;

public class StringToDockConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        return Enum.Parse(typeof(Dock), value.ToString());
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        return value.ToString();
    }
}