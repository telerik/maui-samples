using Microsoft.Maui.Controls;
using System;
using System.Globalization;

namespace QSF.Converters
{
    public class NullOrEmptyToValueConverter : IValueConverter
    {
        public object NullVaule { get; set; }
        public object DefaultVaule { get; set; }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
            {
                return this.NullVaule;
            }
            else if (value is string str && string.IsNullOrEmpty(str))
            {
                return this.NullVaule;
            }
            else
            {
                return this.DefaultVaule;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
