using System;
using System.Globalization;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Graphics;

namespace SDKBrowserMaui.Examples.DataGridControl.DecorationsCategory.GridLinesExample;

public class StringToColorConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value != null)
        {
            string color = value.ToString();
            switch (color)
            {
                case nameof(Colors.Gray):
                    return Colors.Gray;
                case nameof(Colors.Red):
                    return Colors.Red;
                case nameof(Colors.Green):
                    return Colors.Green;
            }
        }

        return Colors.Gray;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}