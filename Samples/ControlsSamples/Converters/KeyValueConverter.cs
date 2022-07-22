using Microsoft.Maui.Controls;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QSF.Converters
{
    [ContentProperty(nameof(Resources))]
    public class KeyValueConverter : IValueConverter
    {
        public ResourceDictionary Resources { get; }

        public KeyValueConverter()
        {
            this.Resources = new ResourceDictionary();
        }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return this.Resources[(string)value];
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return this.Resources
                .Where(pair => pair.Value.Equals(value))
                .Select(pair => pair.Key)
                .FirstOrDefault();
        }
    }
}
