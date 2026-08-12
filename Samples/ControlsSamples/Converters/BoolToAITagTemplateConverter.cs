using Microsoft.Maui.Controls;
using System;
using System.Globalization;

namespace QSF.Converters
{
    public class BoolToAITagTemplateConverter : IValueConverter
    {
        public DataTemplate AITemplate { get; set; }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is bool containsAI && containsAI && this.AITemplate != null)
            {
                return this.AITemplate.CreateContent();
            }

            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
