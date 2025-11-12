using Microsoft.Maui.Controls;
using Microsoft.Maui.Graphics;
using System;
using System.Globalization;
using System.Linq;
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
                    color = GetThemeColor((string)parameter);
                    break;
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

    private Color GetThemeColor(string resourceKey)
    {
        if (string.IsNullOrWhiteSpace(resourceKey))
        {
            return null;
        }

        string resolvedKey = resourceKey switch
        {
            "TrackFill" => resourceKey.Contains("Telerik") ? "RadBorderColor" : "RadProgressBarTrackFillColor",
            "ProgressFill" => "RadPrimaryColor",
            _ => resourceKey
        };

        var swatch = TelerikThemeResources.Current.MergedDictionaries.FirstOrDefault();

        if (swatch != null)
        {
            if (swatch.TryGetValue(resolvedKey, out var colorValue))
            {
                if (colorValue is Color color)
                {
                    return color;
                }
                if (colorValue is OnPlatform<Color> colorOnPlatform)
                {
                    return colorOnPlatform.Default;
                }
            }
        }

        return null;
    }
}
