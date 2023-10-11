using System;
using System.Globalization;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Graphics;

namespace QSF.Examples.TreeViewControl.Common;

public class StringToFontImageSourceConverter : IValueConverter
{
    public string FontFamily { get; set; }
    public Color Color { get; set; }
    public double Size { get; set; }

    public StringToFontImageSourceConverter()
    {
        this.FontFamily = "TelerikFontExamples";
        this.Color = Colors.Black.WithAlpha(0.6f);
        this.Size = 12;
    }

    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is null)
        {
            return null;
        }

        return new FontImageSource()
        {
            Glyph = value.ToString(),
            FontFamily = this.FontFamily,
            Color = this.Color,
            Size = this.Size
        };
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotSupportedException();
    }
}