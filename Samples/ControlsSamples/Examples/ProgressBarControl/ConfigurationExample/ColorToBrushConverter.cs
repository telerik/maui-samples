using Microsoft.Maui.Controls;
using Microsoft.Maui.Graphics;
using System;
using System.Globalization;
using Telerik.Maui.Controls;

namespace QSF.Examples.ProgressBarControl.ConfigurationExample;

public class ColorToBrushConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        string colorString = (string)value;
        Color color = Colors.Black;

        switch (colorString)
        {
            case "Default":
                if (parameter != null)
                {
                    if (parameter.ToString().Equals("ProgressFill"))
                    {
                        color = (RadLinearProgressBar.ProgressFillProperty.DefaultValue as SolidColorBrush).Color;
                        break;
                    }
                    else if (parameter.ToString().Equals("TrackFill"))
                    {
                        color = (RadLinearProgressBar.TrackFillProperty.DefaultValue as SolidColorBrush).Color;
                        break;
                    }
                }

                color = null;
                break;
            case "Black":
                color = Colors.Black;
                break;
            case "White":
                color = Colors.White;
                break;
            case "Red":
                color = Color.FromArgb("#F85446");
                break;
            case "Blue":
                color = Color.FromArgb("#007AFF");
                break;
            case "Green":
                color = Color.FromArgb("#019688");
                break;
            case "Orange":
                color = Color.FromArgb("#FFAC3E");
                break;
            default:
                break;
        }

        return color == null ? null : new SolidColorBrush(color);
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        return null;
    }
}
