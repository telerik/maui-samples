using Microsoft.Maui.Controls;
using System;
using System.Collections.Generic;
using System.Globalization;

namespace QSF.Examples.ChatControl.ChatRoomExample;

public class EmojiConverter : IValueConverter
{
    private List<Tuple<string, string>> emojis = new List<Tuple<string, string>>
    {
        new Tuple<string, string>(":)", "😀"),
        new Tuple<string, string>(":D", "😂"),
        new Tuple<string, string>(":|", "😐"),
        new Tuple<string, string>(":?", "🤔"),
    };

    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        string text = value as string;
        if (!string.IsNullOrEmpty(text))
        {
            foreach (Tuple<string, string> emoji in emojis)
            {
                text = text.Replace(emoji.Item1, emoji.Item2);
            }
        }

        return text;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}
