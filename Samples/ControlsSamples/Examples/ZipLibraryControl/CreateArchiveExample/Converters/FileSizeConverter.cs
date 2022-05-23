using Microsoft.Maui.Controls;
using System;
using System.Globalization;

namespace QSF.Examples.ZipLibraryControl.CreateArchiveExample.Converters
{
    public class FileSizeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            long size = (long)value;

            int kBytes = 1024;
            int mBytes = kBytes * 1024;

            if (size < mBytes)
            {
                var sizeInKB = (double)size / kBytes;
                return string.Format("{0:0.##} KB", sizeInKB);
            }

            var sizeInMB = (double)size / mBytes;
            return string.Format("{0:0.##} MB", sizeInMB);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
