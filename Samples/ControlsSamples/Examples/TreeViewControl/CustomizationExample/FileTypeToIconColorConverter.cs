using System;
using System.Globalization;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Graphics;

namespace QSF.Examples.TreeViewControl.CustomizationExample;

public class FileTypeToIconColorConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        string fileType = (value as string).Split('.')[1];

        return fileType switch
        {
            "docx" => Color.FromArgb("#4C69B9"),
            "pdf" => Color.FromArgb("#BD2427"),
            "png" => Color.FromArgb("#D7629B"),
            "ppt" => Color.FromArgb("#E0552F"),
            "mov" => Color.FromArgb("#837AE4"),
            "xlsx" => Color.FromArgb("#4B8B39"),
            _ => Colors.Black,
        };
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => Colors.Black;
}