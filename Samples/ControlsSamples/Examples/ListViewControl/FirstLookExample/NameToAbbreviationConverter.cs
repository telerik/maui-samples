using Microsoft.Maui.Controls;
using System;
using System.Globalization;
using System.Linq;

namespace QSF.Examples.ListViewControl.FirstLookExample
{
    public class NameToAbbreviationConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string[] names = value.ToString().Split(' ');
            return string.Format("{0}", names[0].ToCharArray().ElementAt(0));
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
