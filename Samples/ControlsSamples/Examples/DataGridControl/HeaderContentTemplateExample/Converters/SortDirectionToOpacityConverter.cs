using System;
using Microsoft.Maui.Controls;
using Telerik.Maui.Controls.Compatibility.DataGrid;

namespace QSF.Examples.DataGridControl.HeaderContentTemplateExample;

public class SortDirectionToOpacityConverter : IValueConverter
{

    public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
    {
        var direction = (SortDirection)value;
        switch (direction)
        {
            case SortDirection.Ascending:
            case SortDirection.Descending:
                return 1.0;
            default:
                return 0.0;
        }
    }

    public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        => 0.0;
}