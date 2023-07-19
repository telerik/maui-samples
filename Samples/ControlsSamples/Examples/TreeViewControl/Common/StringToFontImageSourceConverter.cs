using System;
using System.Globalization;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Graphics;

namespace QSF.Examples.TreeViewControl.Common;

public class StringToFontImageSourceConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        return new FontImageSource()
        {
            Glyph = value.ToString(),
            FontFamily = "TelerikFontExamples",
            Size = 12,
            Color = Colors.Black.WithAlpha(0.6f)
        };
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => value;
}