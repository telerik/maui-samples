using Microsoft.Maui.Controls;
using Microsoft.Maui.Graphics;
using System.Globalization;
using System.Linq;

namespace QSF.Converters;

public class LoaderImageConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is double progress)
        {
            int imageIndex = (int)(progress / 30);

            if (progress > 95)
            {
                imageIndex = 4;
            }

            string imageName = imageIndex switch
            {
                0 => "settingsloaderblue.png",
                1 => "settingsloadergreen.png",
                2 => "settingsloaderorange.png",
                3 => "settingsloaderlightpurple.png",
                4 => "settingsloaderpurple.png",
                _ => "settingsloaderblue.png"
            };

            return ImageSource.FromFile(imageName);
        }

        return ImageSource.FromFile("settingsloaderblue.png");
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}