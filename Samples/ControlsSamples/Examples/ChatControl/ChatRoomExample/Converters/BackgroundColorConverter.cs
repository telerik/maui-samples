using System;
using System.Collections.Generic;
using System.Globalization;
using System.Windows;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Graphics;

namespace QSF.Examples.ChatControl.ChatRoomExample;

public class BackgroundColorConverter : IValueConverter
{
    private List<Color> colors;
    private List<string> colorKeys;
    private Dictionary<object, Color> dict;

    private Dictionary<object, string> dictKey;

    public BackgroundColorConverter()
    {
        this.colors = new List<Color>();
        this.colorKeys = new List<string>();
        this.dict = new Dictionary<object, Color>();
        this.dictKey = new Dictionary<object, string>();
    }

    public List<Color> Colors
    {
        get
        {
            return this.colors;
        }
    }

    public List<string> ColorKeys
    {
        get
        {
            return this.colorKeys;
        }
    }

    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value == null)
        {
            return default(Color);
        }

        if (dictKey.TryGetValue(value, out string cachedColorKey))
        {
            if (Application.Current?.Resources != null &&
                Application.Current.Resources.TryGetValue(cachedColorKey, out var resource) &&
                resource is Color color)
            {
                return color;
            }
        }
        else
        {
            string colorKey = this.GetColorKey(dictKey.Count);
            if (!string.IsNullOrEmpty(colorKey))
            {
                dictKey[value] = colorKey;
                
                if (Application.Current?.Resources != null &&
                    Application.Current.Resources.TryGetValue(colorKey, out var resource) &&
                    resource is Color resourceColor)
                {
                    return resourceColor;
                }
            }
        }

        return default(Color);
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }

    private string GetColorKey(int index)
    {
        if (this.colorKeys.Count == 0)
        {
            return null;
        }

        return this.colorKeys[index % this.colorKeys.Count];
    }

    // Update the existing GetColor method to use GetColorKey
    private Color GetColor(int index)
    {
        string resourceKey = this.GetColorKey(index);
        if (string.IsNullOrEmpty(resourceKey))
        {
            return default(Color);
        }

        if (Application.Current?.Resources != null &&
            Application.Current.Resources.TryGetValue(resourceKey, out var resource) &&
            resource is Color color)
        {
            return color;
        }

        return default(Color);
    }
}