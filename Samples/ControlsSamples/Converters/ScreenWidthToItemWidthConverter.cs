using Microsoft.Maui.Controls;
using System;
using System.Globalization;

namespace QSF.Converters;

public class ScreenWidthToItemWidthConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        double screenWidth = (double)value;
        int itemsCount = 1;

        if (screenWidth < 641)
        {
            itemsCount = 1;
        }
        else if (screenWidth < 1008)
        {
            itemsCount = 2;
        }
        else if (screenWidth < 1582)
        {
            itemsCount = 3;
        }
        else
        {
            itemsCount = 4;
        }

#if WINDOWS
        double layoutEpsilon = 2;
#else
        double layoutEpsilon = 0.1;
#endif

        double itemWidth = (screenWidth / itemsCount) - layoutEpsilon;
        return itemWidth;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        return value;
    }
}
