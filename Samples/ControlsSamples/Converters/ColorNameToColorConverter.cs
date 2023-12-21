using Microsoft.Maui.Controls;
using Microsoft.Maui.Graphics;
using System;
using System.Globalization;

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
                if (parameter != null && this.DefaultValuesResourceDictionary != null 
                    && this.DefaultValuesResourceDictionary.ContainsKey((string)parameter))
                {
                    color = this.DefaultValuesResourceDictionary[(string)parameter] as Color;
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
}
