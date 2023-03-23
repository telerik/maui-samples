using Microsoft.Maui.Controls;
using QSF.Common;
using System;
using System.Globalization;

namespace QSF.Converters
{
    public class StatusTagToTemplateConverter : IValueConverter
    {
        public DataTemplate NormalTemplate { get; set; }
        public DataTemplate NewTemplate { get; set; }
        public DataTemplate UpdatedTemplate { get; set; }
        public DataTemplate BetaTemplate { get; set; }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            DataTemplate statusTagTemplate = this.NormalTemplate;

            switch ((StatusType)value)
            {
                case StatusType.New:
                    statusTagTemplate = this.NewTemplate;
                    break;
                case StatusType.Updated:
                    statusTagTemplate = this.UpdatedTemplate;
                    break;
                case StatusType.Beta:
                    statusTagTemplate = this.BetaTemplate;
                    break;
                default:
                    break;
            }

            return statusTagTemplate.CreateContent();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
