using System;
using System.Globalization;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Graphics;

namespace CryptoTracker.Converters
{
    public class ValueToColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var doubleValue = (double)value;

            if (doubleValue > 0)
            {
                return this.PositiveColor;
            }

            if (doubleValue < 0)
            {
                return this.NegativeColor;
            }

            return this.NeutralColor;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }

        public Color PositiveColor { get; set; }
        public Color NegativeColor { get; set; }
        public Color NeutralColor { get; set; }
    }
}
