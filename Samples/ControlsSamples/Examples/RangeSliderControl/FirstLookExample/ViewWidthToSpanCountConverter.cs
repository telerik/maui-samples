using System;
using System.Globalization;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Devices;

namespace QSF.Examples.RangeSliderControl.FirstLookExample;

public class ViewWidthToSpanCountConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        double viewWidth = (double)value;
        int itemsCount;

        if (viewWidth < 641)
        {
            itemsCount = 1;
        }
        else if (viewWidth < 1008)
        {
            itemsCount = 2;
        }
        else if (viewWidth < 1582)
        {
            itemsCount = 3;
        }
        else
        {
            itemsCount = 4;
        }

        return itemsCount;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        var platform = DeviceInfo.Platform;
        return platform == DevicePlatform.MacCatalyst || platform == DevicePlatform.WinUI ? 2 : 1;
    }
}
