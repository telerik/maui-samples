using Microsoft.Maui.Controls;
using Microsoft.Maui.Graphics;
using QSF.ViewModels;
using System;
using System.Globalization;

namespace QSF.Converters;

public class HightlitedTextConverter : IValueConverter
{
    public Color HighlightTextColor { get; set; }

    public Color HighlightBackgroundColor { get; set; }

    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        HighlightedText highlightedText = (HighlightedText)value;
        if (highlightedText == null)
        {
            return null;
        }

        FormattedString result = new FormattedString();

        if (highlightedText.HighlightLength > 0)
        {
            int start = highlightedText.HighlightStart;
            int length = highlightedText.HighlightLength;

            string firstPart = highlightedText.Text.Substring(0, start);
            if (!string.IsNullOrEmpty(firstPart))
            {
                result.Spans.Add(new Span { Text = firstPart });
            }

            var middlePart = highlightedText.Text.Substring(start, length);
            if (!string.IsNullOrEmpty(middlePart))
            {
                result.Spans.Add(new Span { Text = middlePart, TextColor = this.HighlightTextColor, BackgroundColor = this.HighlightBackgroundColor });
            }

            var lastPart = highlightedText.Text.Substring(start + length);
            if (!string.IsNullOrEmpty(lastPart))
            {
                result.Spans.Add(new Span { Text = lastPart });
            }
        }
        else
        {
            result.Spans.Add(new Span { Text = highlightedText.Text });
        }

        return result;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}
