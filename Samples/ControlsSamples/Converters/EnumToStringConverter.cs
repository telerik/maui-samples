using Microsoft.Maui.Controls;
using System;
using System.Globalization;

namespace QSF.Converters
{
    public class EnumToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return Convert(value);
        }

        internal static string Convert(object value)
        {
            if (value == null)
            {
                return string.Empty;
            }

            var stringValue = value.ToString();

            return stringValue.InsertSpacesInPascalCase();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
