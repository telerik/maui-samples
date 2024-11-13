using Microsoft.Maui.Controls;
using QSF.Common;
using QSF.Helpers;
using QSF.ViewModels;
using System.Globalization;

namespace QSF.Converters;

public class ModelToViewConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        var createBindingContext = parameter as bool?;
        if (value is ExampleViewModel viewModel)
        {
            return Utils.CreateView(viewModel.Example, createBindingContext ?? false);
        }
        else if (value is Example example)
        {
            return Utils.CreateView(example, createBindingContext ?? true);
        }
        else if (value is Control control)
        {
            return Utils.CreateView(control);
        }
        else
        {
            return null;
        }
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}
