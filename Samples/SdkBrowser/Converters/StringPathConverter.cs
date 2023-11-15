using System;
using System.Collections;
using System.Globalization;
using System.Linq;
using Microsoft.Maui.Controls;

namespace SDKBrowserMaui.Converters
{
    // >> string-path-converter
    public class StringPathConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is string path)
            {
                return path.Split('/', '\\', StringSplitOptions.RemoveEmptyEntries);
            }

            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is IEnumerable path)
            {
                var items = path.OfType<object>();

                return string.Join("/", items);
            }

            return null;
        }
    }
    // << string-path-converter
}
