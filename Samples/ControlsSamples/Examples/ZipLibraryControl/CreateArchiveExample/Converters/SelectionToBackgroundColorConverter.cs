using Microsoft.Maui.Controls;
using Microsoft.Maui.Graphics;
using System;
using System.Globalization;
using QSF.ViewModels;

namespace QSF.Examples.ZipLibraryControl.CreateArchiveExample.Converters
{
    public class SelectionToBackgroundColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool isSelected = (bool)value;
            bool isDark = ThemingViewModel.IsDarkMode;

            if (isSelected)
            {
                return isDark
                    ? new Color(0.196f, 0.196f, 0.196f)
                    : new Color(0.9f, 0.9f, 0.9f, 0.5f);
            }

            return Color.FromArgb("00FFFFFF");
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
