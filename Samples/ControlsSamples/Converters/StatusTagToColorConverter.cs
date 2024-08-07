using Microsoft.Maui.Controls;
using Microsoft.Maui.Graphics;
using QSF.Common;
using System;
using System.Globalization;

namespace QSF.Converters;

public class StatusTagToColorConverter : IValueConverter
{
    public Color NewColor { get; set; }
    public Color UpdatedColor { get; set; }
    public Color BetaColor { get; set; }

    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        switch ((StatusType)value)
        {
            case StatusType.New:
                return this.NewColor;
            case StatusType.Updated:
                return this.UpdatedColor;
            case StatusType.Beta:
                return this.BetaColor;
            default:
                break;
        }

        return null;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        => throw new NotImplementedException();
}