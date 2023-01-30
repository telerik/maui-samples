using Microsoft.Maui.Controls;
using System;
using System.Globalization;

namespace QSF.Converters
{
    public class NullOrEmptyToValueConverter : IValueConverter
    {
        public object NullValue { get; set; }
        public object DefaultValue { get; set; }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
            {
                return this.NullValue;
            }
            else if (value is string str && string.IsNullOrEmpty(str))
            {
                return this.NullValue;
            }
            else
            {
                return this.DefaultValue;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
