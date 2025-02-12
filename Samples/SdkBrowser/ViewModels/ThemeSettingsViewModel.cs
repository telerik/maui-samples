using Microsoft.Maui.ApplicationModel;
using Microsoft.Maui.Controls;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.Json;
using System.Text.Json.Serialization;
using Telerik.Maui.Controls;

namespace SDKBrowserMaui.ViewModels;

public class ThemeSettingsViewModel : NotifyPropertyChangedBase
{
    public ThemeSettingsViewModel()
    {
        string result = null;

        Assembly assembly = Assembly.GetExecutingAssembly();
        using (Stream stream = assembly.GetManifestResourceStream("SDKBrowserMaui.Common.Catalog.json"))
        {
            using (StreamReader reader = new StreamReader(stream))
            {
                result = reader.ReadToEnd();
            }
        }

        Console.WriteLine(result);

        var themeStrings = TelerikTheming.Themes.Select(t => t.FullName).ToHashSet();

        var themeDefinitions = JsonSerializer.Deserialize<ThemeDefinition[]>(result);

        this.ThemesCatalog = themeDefinitions
            .SelectMany(theme => theme.Swatches)
            .Where(theme => themeStrings.Contains(string.IsNullOrEmpty(theme.Swatch) ? theme.Theme : theme.Theme + " " + theme.Swatch))
            .ToArray();
        
        this.HeaderLabel = "Theme Settings";

        this.UserAppThemeOptions = new ReadOnlyCollection<string>(new List<string> {
            "Auto",
            "Light",
            "Dark"
        });

        Application.Current.RequestedThemeChanged += this.OnRequestedThemeChanged;
    }

    public SwatchDefinition CurrentTheme
    {
        get
        {
            var telerikTheming = App.Current.Resources.MergedDictionaries.OfType<TelerikTheming>().Single();

            var swatchDefinition = this.ThemesCatalog.Single(catalogTheme =>
                catalogTheme.Theme == telerikTheming.Theme.Theme &&
                catalogTheme.Swatch == telerikTheming.Theme.Swatch);

            return swatchDefinition;
        }

        set
        {
            if (value == null || value == this.CurrentTheme)
            {
                return;
            }

            // Console.WriteLine("  Will try to find the theme RD...");
            var telerikTheming = App.Current.Resources.MergedDictionaries.OfType<TelerikTheming>().Single();

            // Console.WriteLine("  Find ThemeKey from SwatchDefinition " + value.Theme + " " + value.Swatch);
            
            var themeKey = TelerikTheming.Themes.Single(themeKey =>
                themeKey.Theme == value.Theme &&
                themeKey.Swatch == value.Swatch);
            // Console.WriteLine("Found: " + themeKey.FullName);

            // Console.WriteLine("  Switch the theme...");
            telerikTheming.Theme = themeKey;

            Console.WriteLine($"Set theme to {(value == null ? "null" : themeKey.Theme + " " + themeKey.Swatch)}.");
            this.OnPropertyChanged();
        }
    }

    public SwatchDefinition[] ThemesCatalog { get; private set; }

    public string HeaderLabel { get; private set; }

    public ICollection<string> UserAppThemeOptions { get; private set; }

    public string UserAppTheme
    {
        get
        {
            switch(Application.Current.UserAppTheme)
            {
                case AppTheme.Light: return "Light";
                case AppTheme.Dark: return "Dark";
                default: return "Auto";
            }
        }

        set
        {
            switch(value)
            {
                case "Light":
                    Application.Current.UserAppTheme = Microsoft.Maui.ApplicationModel.AppTheme.Light;
                    break;
                case "Dark":
                    Application.Current.UserAppTheme = Microsoft.Maui.ApplicationModel.AppTheme.Dark;
                    break;
                case "Unspecified":
                case "Platform":
                case "OS":
                case "Auto":
                default:
                    Application.Current.UserAppTheme = Microsoft.Maui.ApplicationModel.AppTheme.Unspecified;
                    break;
            }

            this.OnRequestedThemeChanged(this, null);
        }
    }

    public string RequestedTheme => AppThemeToString(Application.Current.RequestedTheme);

    public string PlatformAppTheme => AppThemeToString(Application.Current.PlatformAppTheme);

    public static string AppThemeToString(AppTheme theme)
    {
        switch(theme)
        {
            case Microsoft.Maui.ApplicationModel.AppTheme.Light:
                return "Light";
            case Microsoft.Maui.ApplicationModel.AppTheme.Dark:
                return "Dark";
            case Microsoft.Maui.ApplicationModel.AppTheme.Unspecified:
            default:
                return "Unspecified";
        }
    }

    private void OnRequestedThemeChanged(object sender, AppThemeChangedEventArgs args)
    {
        this.OnPropertyChanged("UserAppTheme");
        this.OnPropertyChanged("RequestedTheme");
        this.OnPropertyChanged("PlatformAppTheme");
    }

    public class ThemeDefinition
    {
        [JsonPropertyName("theme")]
        public string Theme { get; set; }

        [JsonPropertyName("swatches")]
        public SwatchDefinition[] Swatches { get; set; }
    }

    public class SwatchDefinition
    {
        [JsonPropertyName("theme")]
        public string Theme { get; set; }

        [JsonPropertyName("swatch")]
        public string Swatch { get; set; }

        public string DisplayFullName => $"{this.Theme} {this.Swatch}";

        [JsonPropertyName("previewColors")]
        public string[] PreviewColors { get; set; }
    }
}
