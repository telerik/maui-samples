using Microsoft.Maui.Controls;
using Microsoft.Maui.Devices;
using System;
using System.Globalization;

namespace SDKBrowserMaui.Converters
{
    public class ImageSourceConverter : IValueConverter
    {
        public object Convert(object value, Type type, object parameter, CultureInfo culture)
        {
            if (value != null && DeviceInfo.Platform == DevicePlatform.WinUI)
            {
                return string.Format("Assets\\{0}", value);
            }

            return value;
        }

        public object ConvertBack(object value, Type type, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}
