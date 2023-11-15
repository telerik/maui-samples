using System;
using System.Globalization;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Graphics;

namespace QSF.Converters;

public class StringToFontImageSourceConverter : IValueConverter
{
    public string FontFamily { get; set; }
    public Color Color { get; set; }
    public double Size { get; set; }

    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is string glyph)
        {
            return new FontImageSource
            {
                Glyph = glyph,
                FontFamily = this.FontFamily,
                Size = this.Size,
                Color = this.Color
            };
        }

        return null;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is FontImageSource imageSource)
        {
            return imageSource.Glyph;
        }

        return null;
    }
}