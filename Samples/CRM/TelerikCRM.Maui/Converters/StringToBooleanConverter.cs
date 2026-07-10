using System.Globalization;

namespace TelerikCRM.Maui.Converters;

public class StringToBooleanConverter : IValueConverter
{
    public StringComparisonMethod ComparisonMethod { get; set; }

    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value == null || parameter == null)
        {
            return false;
        }

        try
        {
            string selectedString = value.ToString()?.ToLowerInvariant();
            string compareToString = parameter.ToString()?.ToLowerInvariant();

            if(compareToString == null)
            {
                return false;
            }

            return this.ComparisonMethod switch
            {
                StringComparisonMethod.Equals => selectedString?.Equals(compareToString),
                StringComparisonMethod.StartsWith => selectedString?.StartsWith(compareToString),
                StringComparisonMethod.EndsWith => selectedString?.EndsWith(compareToString),
                StringComparisonMethod.Contains => selectedString?.Contains(compareToString),
                _ => false
            };
        }
        catch (Exception e)
        {
            Console.WriteLine($"StringToBooleanConverter encountered an error: {e.Message}");
            throw;
        }
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        => throw new NotImplementedException();
}

public enum StringComparisonMethod
{
    Equals,
    StartsWith,
    EndsWith,
    Contains
}