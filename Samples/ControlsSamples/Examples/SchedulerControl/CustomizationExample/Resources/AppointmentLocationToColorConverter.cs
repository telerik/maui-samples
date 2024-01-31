using System;
using System.Globalization;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Devices;
using Microsoft.Maui.Graphics;

namespace QSF.Examples.SchedulerControl.CustomizationExample;

public class AppointmentLocationToColorConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value == null)
        {
            return Colors.Transparent;
        }

        bool shouldApplyAlpha = !(parameter != null && parameter.ToString().Equals("Category"));
        bool isWindows = DeviceInfo.Platform == DevicePlatform.WinUI;

        switch(value)
        {
            case "Sofia 501":
                Color location501Color = Color.FromArgb("#0066FF");
                Color location501ColorWithAlpha = isWindows ? Color.FromArgb("#B2D1FF") : location501Color.WithAlpha(0.3f);
                return shouldApplyAlpha ? location501ColorWithAlpha : location501Color;
            case "Sofia 502":
                Color location502Color = Color.FromArgb("#339999");
                Color location502ColorWithAlpha = isWindows ? Color.FromArgb("#C2E0DF") : location502Color.WithAlpha(0.3f);
                return shouldApplyAlpha ? location502ColorWithAlpha : location502Color;
            case "Sofia 503":
                Color location503Color = Color.FromArgb("#6633CC");
                Color location503ColorWithAlpha = isWindows ? Color.FromArgb("#D1C1F1") : location503Color.WithAlpha(0.3f);
                return shouldApplyAlpha ? location503ColorWithAlpha : location503Color;
            default:
                return Colors.Transparent;
        }
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        => throw new NotImplementedException();
}