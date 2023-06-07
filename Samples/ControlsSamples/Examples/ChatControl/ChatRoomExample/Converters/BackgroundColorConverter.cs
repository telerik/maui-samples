using System;
using System.Collections.Generic;
using System.Globalization;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Graphics;

namespace QSF.Examples.ChatControl.ChatRoomExample;

public class BackgroundColorConverter : IValueConverter
{
    private List<Color> colors;
    private Dictionary<object, Color> dict;

    public BackgroundColorConverter()
    {
        this.colors = new List<Color>();
        this.dict = new Dictionary<object, Color>();
    }

    public List<Color> Colors
    {
        get
        {
            return this.colors;
        }
    }

    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value == null)
        {
            return default(Color);
        }

        Color color;
        if (!dict.TryGetValue(value, out color))
        {
            color = this.GetColor(dict.Count);
            dict[value] = color;
        }

        return color;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }

    private Color GetColor(int index)
    {
        if (this.colors.Count == 0)
        {
            return default(Color);
        }

        return this.colors[index % this.colors.Count];
    }
}