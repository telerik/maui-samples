using System.Globalization;

namespace TelerikCRM.Maui.Converters;

internal class IntToDoubleConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is int val)
        {
            return (double)val;
        }

        return null;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is double val)
        {
            return System.Convert.ToInt32(val);
        }

        return null;
    }
}