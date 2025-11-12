using Microsoft.Maui.Graphics;
using System;
using System.Reflection;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Controls.Xaml;
using QSF;

namespace QSF.MarkupExtensions
{
    [ContentProperty(nameof(Default))]
    public class TelerikThemeExtension : IMarkupExtension
    {
        public object PlatformLight { get; set; }
        public object PlatformDark { get; set; }
        public object TelerikMain { get; set; }
        public object TelerikMainDark { get; set; }
        public object TelerikOceanBlue { get; set; }
        public object TelerikOceanBlueDark { get; set; }
        public object TelerikPurple { get; set; }
        public object TelerikPurpleDark { get; set; }
        public object TelerikTurquoise { get; set; }
        public object TelerikTurquoiseDark { get; set; }
        public object Default { get; set; }

        public object ProvideValue(IServiceProvider serviceProvider)
        {
            var theme = TelerikThemeResources.AppTheme;
            return theme switch
            {
                TelerikTheme.PlatformLight => PlatformLight ?? Default,
                TelerikTheme.PlatformDark => PlatformDark ?? Default,
                TelerikTheme.TelerikMain => TelerikMain ?? Default,
                TelerikTheme.TelerikMainDark => TelerikMainDark ?? Default,
                TelerikTheme.TelerikOceanBlue => TelerikOceanBlue ?? Default,
                TelerikTheme.TelerikOceanBlueDark => TelerikOceanBlueDark ?? Default,
                TelerikTheme.TelerikPurple => TelerikPurple ?? Default,
                TelerikTheme.TelerikPurpleDark => TelerikPurpleDark ?? Default,
                TelerikTheme.TelerikTurquoise => TelerikTurquoise ?? Default,
                TelerikTheme.TelerikTurquoiseDark => TelerikTurquoiseDark ?? Default,
                _ => Default
            };
        }
    }
}
