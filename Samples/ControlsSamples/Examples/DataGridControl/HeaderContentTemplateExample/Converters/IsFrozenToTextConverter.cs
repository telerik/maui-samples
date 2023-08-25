using System;
using Microsoft.Maui.Controls;
using Telerik.Maui;

namespace QSF.Examples.DataGridControl.HeaderContentTemplateExample;

public class IsFrozenToTextConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
    {
        return (bool)value ? "\ue88e" : "\ue88f";
    }

    public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        => string.Empty;
}
