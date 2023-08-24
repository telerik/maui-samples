using System;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Graphics;
using Telerik.Maui.Controls.Compatibility.DataGrid;

namespace QSF.Examples.DataGridControl.HeaderContentTemplateExample;

public class SortDirectionToColorConverter : IValueConverter
{
    public Color AscendingColor { get; set; }
    public Color DescendingColor { get; set; }
    public Color NoneColor { get; set; }

    public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
    {
        var direction = (SortDirection)value;
        switch (direction)
        {
            case SortDirection.Ascending:
                return this.AscendingColor;
            case SortDirection.Descending:
                return this.DescendingColor;
            default:
                return this.NoneColor;
        }
    }

    public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        => Colors.Transparent;
}