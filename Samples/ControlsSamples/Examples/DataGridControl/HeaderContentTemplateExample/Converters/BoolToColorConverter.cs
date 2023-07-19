using System;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Graphics;

namespace QSF.Examples.DataGridControl.HeaderContentTemplateExample;

public class BoolToColorConverter : IValueConverter
{
    public Color TrueColor { get; set; }
    public Color FalseColor { get; set; }

    public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        => (bool)value ? this.TrueColor : this.FalseColor;

    public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        => Colors.Transparent;
}