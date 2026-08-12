using Microsoft.Maui.Controls;
using System.Globalization;

namespace QSF.Examples.AutoCompleteControl.ConfigurationExample;

public class StringToDoubleConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        string stringValue = value as string;

        if  (stringValue == null)
        {
            return -1;
        }

        return stringValue == "Default" ? -1 : double.Parse(stringValue);
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}