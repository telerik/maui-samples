using Microsoft.Maui.Controls;
using System;
using System.ComponentModel;
using System.Globalization;

namespace QSF.Converters
{
    [ContentProperty(nameof(TypeConverter))]
    public class ValueTypeConverter : IValueConverter
    {
        public TypeConverter TypeConverter { get; set; }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var text = (string)value;

            if (string.IsNullOrEmpty(text))
            {
                return null;
            }

            return this.TypeConverter.ConvertFromInvariantString(text);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return this.TypeConverter.ConvertToInvariantString(value);
        }
    }
}
