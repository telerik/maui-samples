using Microsoft.Maui.Controls;
using Microsoft.Maui.Graphics;
using System.Globalization;
using System.Linq;

namespace QSF.Converters;

public class ColorNameToColorConverter : IValueConverter
{
    public ResourceDictionary DefaultValuesResourceDictionary { get; set; }

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

        return color;
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
            "BackTrackColor" => resourceKey.Contains("Telerik") ? "RadBorderAltColor" : "RadSliderBackTrackColor",
            "InRangeTickColor" => resourceKey.Contains("Telerik") ? "RadBorderColor" : "RadSliderMiddleTickBackgroundColor",
            "OutOfRangeTickColor" => resourceKey.Contains("Telerik") ? "RadBorderColor" : "RadSliderOutOfRangeTickBackgroundColor",
            "TextColor" => "RadOnAppSurfaceColor",
            "ThumbFill" => resourceKey.Contains("Telerik") ? "RadPrimaryColor" : "RadSliderThumbFillColor",
            "StartThumbFill" => resourceKey.Contains("Telerik") ? "RadPrimaryColor" : "RadSliderThumbFillColor",
            "EndThumbFill" => resourceKey.Contains("Telerik") ? "RadPrimaryColor" : "RadSliderThumbFillColor",
            "RangeTrackFill" => "RadPrimaryColor",
            "HighlightTextColor" => "RadPrimaryColor",
            "ProgressFill" => "RadPrimaryColor",
            "TrackFill" => "RadBorderColor",
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
