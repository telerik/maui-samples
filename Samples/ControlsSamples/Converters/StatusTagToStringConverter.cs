using Microsoft.Maui.Controls;
using QSF.Common;
using System;
using System.Globalization;

namespace QSF.Converters;

public class StatusTagToStringConverter : IValueConverter
{
    public string NewValue { get; set; }
    public string UpdatedValue { get; set; }
    public string BetaValue { get; set; }

    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        switch ((StatusType)value)
        {
            case StatusType.New:
                return this.NewValue;
            case StatusType.Updated:
                return this.UpdatedValue;
            case StatusType.Beta:
                return this.BetaValue;
            default:
                break;
        }

        return null;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        => throw new NotImplementedException();
}