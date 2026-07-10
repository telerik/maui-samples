using System.Globalization;
using Telerik.Maui.Controls.NavigationView;
using TelerikCRM.Maui.Models;

namespace TelerikCRM.Maui.Converters;

public class PageModelToViewConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        var services = Application.Current?.Windows.FirstOrDefault()?.Page?.Handler?.MauiContext?.Services;

        if (services == null)
        {
            return null;
        }

        if (value is PageModel model)
        {
            return services.GetService(model.Type);
        }

#if MACCATALYST || WINDOWS
        return value is NavigationViewItem { Text: "About" } 
            ? services.GetService(typeof(Views.Desktop.AboutView)) 
            : null;
#endif
        return null;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        => throw new NotImplementedException();
}