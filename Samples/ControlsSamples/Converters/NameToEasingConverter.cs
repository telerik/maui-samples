using System;
using System.Collections.Generic;
using System.Globalization;
using Microsoft.Maui;
using Microsoft.Maui.Controls;

namespace QSF.Converters
{
    public class NameToEasingConverter : IValueConverter
    {
        private static readonly Dictionary<string, Easing> nameToEasing;
        private static readonly Dictionary<Easing, string> easingToName;

        static NameToEasingConverter()
        {
            nameToEasing = new Dictionary<string, Easing>();
            easingToName = new Dictionary<Easing, string>();

            AddPair(nameof(Easing.BounceIn), Easing.BounceIn);
            AddPair(nameof(Easing.BounceOut), Easing.BounceOut);
            AddPair(nameof(Easing.CubicIn), Easing.CubicIn);
            AddPair(nameof(Easing.CubicInOut), Easing.CubicInOut);
            AddPair(nameof(Easing.CubicOut), Easing.CubicOut);
            AddPair(nameof(Easing.Linear), Easing.Linear);
            AddPair(nameof(Easing.SinIn), Easing.SinIn);
            AddPair(nameof(Easing.SinInOut), Easing.SinInOut);
            AddPair(nameof(Easing.SinOut), Easing.SinOut);
            AddPair(nameof(Easing.SpringIn), Easing.SpringIn);
            AddPair(nameof(Easing.SpringOut), Easing.SpringOut);
        }

        private static void AddPair(string name, Easing easing)
        {
            nameToEasing.Add(name, easing);
            easingToName.Add(easing, name);
        }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Easing color = (Easing)value;

            return easingToName[color];
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string name = (string)value;

            return nameToEasing[name];
        }
    }
}
