using Microsoft.Maui.Controls;
using QSF.Helpers;
using QSF.ViewModels;
using System.Globalization;

namespace QSF.Converters;

public class ModelToConfigurationAreaConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is ExampleViewModel viewModel && viewModel.Example != null && viewModel.Example.IsConfigurable)
        {
            return Utils.CreateConfigurationArea(viewModel.Example);
        }

        return null;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}
