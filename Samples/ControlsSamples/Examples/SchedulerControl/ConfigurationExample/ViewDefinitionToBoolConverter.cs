using System;
using System.Globalization;
using Microsoft.Maui.Controls;

namespace QSF.Examples.SchedulerControl.ConfigurationExample;

public class ViewDefinitionToBoolConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value == null)
        {
            return true;
        }

        string activeViewDefinitionName = value.GetType().Name;
        return !parameter.ToString().Contains(activeViewDefinitionName);
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        => throw new NotImplementedException();
}