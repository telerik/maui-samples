using System;
using Microsoft.Maui.Controls;
using Telerik.Maui;
using Telerik.Maui.Controls.Compatibility.DataGrid;

namespace QSF.Examples.DataGridControl.HeaderContentTemplateExample;

public class SortDirectionToTextConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
    {
        var direction = (SortDirection)value;
        switch (direction)
        {
            case SortDirection.Ascending:
                return "\ue811";
            case SortDirection.Descending:
                return "\ue80d";
            default:
                return string.Empty;
        }
    }

    public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        => string.Empty;
}