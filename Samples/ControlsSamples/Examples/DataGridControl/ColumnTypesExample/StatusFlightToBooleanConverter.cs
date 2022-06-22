using Microsoft.Maui.Controls;
using System;
using System.Globalization;

namespace QSF.Examples.DataGridControl.ColumnTypesExample
{
    public class StatusFlightToBooleanConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var data = (string)value;

            if (data == "Departed")
            {
                return true;
            }
            else return false;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
