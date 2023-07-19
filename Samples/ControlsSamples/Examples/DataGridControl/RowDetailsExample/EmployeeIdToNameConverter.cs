using System;
using System.Globalization;
using Microsoft.Maui.Controls;

namespace QSF.Examples.DataGridControl.RowDetailsExample;

public class EmployeeIdToNameConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        switch (value.ToString())
        {
            case "1":
                return "Amy Alberts";
            case "2":
                return "David Barber";
            case "3":
                return "Edward Parker";
            case "4":
                return "Isabella Barnes";
            case "5":
                return "Warren Pal";
            case "6":
                return "Francis Gill";
            case "7":
                return "Laura Xu";
            case "8":
                return "Natalie Bailey";
            case "9":
                return "Bryan Baker";
            default:
                return String.Empty;
        }
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        return String.Empty;
    }
}
