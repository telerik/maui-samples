using Microsoft.Maui.Controls;
using Microsoft.Maui.Graphics;
using System.Globalization;

namespace QSF.Converters;

public class ResourceKeyToColorConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is string resourceKey && Application.Current?.Resources != null)
        {
            if (Application.Current.Resources.TryGetValue(resourceKey, out var resource) && resource is Color color)
            {
                return color;
            }
        }
        return Colors.Gray;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}