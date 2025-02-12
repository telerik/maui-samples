using Microsoft.Maui.ApplicationModel;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Graphics;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace QSF.ViewModels;

public class ThemeSettingsViewModel : ViewModelBase
{
    private ICollection<ResourceDictionary> mergedDictionaries;

    private static bool prefetchStarted = false;
    private static bool prefetched = false;
    private static double progress = 0;

    // TODO: Delete this once we apply the platform theme on an App level.
    private static ResourceDictionary defaultSwatch;

    static ThemeSettingsViewModel()
    {
        defaultSwatch = new ResourceDictionary();
        defaultSwatch.Add("RadAppSurfaceColor", Colors.White);
        defaultSwatch.Add("RadOnAppSurfaceColor", Colors.Black);
#if WINDOWS
        defaultSwatch.Add("RadBorderColor", Color.FromArgb("#0F000000"));
#else
        defaultSwatch.Add("RadBorderColor", Color.FromArgb("#DFDFDF"));
#endif
    }

    public ThemeSettingsViewModel()
    {
        string result = null;

        Assembly assembly = Assembly.GetExecutingAssembly();
        using (Stream stream = assembly.GetManifestResourceStream("QSF.ViewModels.Themes.Catalog.json"))
        {
            using (StreamReader reader = new StreamReader(stream))
            {
                result = reader.ReadToEnd();
            }
        }

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

        App.Current.Resources.MergedDictionaries.Remove(defaultSwatch);

        Application.Current.RequestedThemeChanged += this.OnRequestedThemeChanged;
    }

    public bool PrefetchStarted = prefetchStarted;
    public bool Prefetched => prefetched;
    public bool Loading => prefetchStarted && !prefetched;

    public double Progress => progress;

    public async Task PrefetchAsync()
    {
        if (!prefetchStarted)
        {
            prefetchStarted = true;
            this.OnPropertyChanged("PrefetchStarted");
            this.OnPropertyChanged("Loading");

            // NOTE: This is instance, while the progress is static...
            // may have minor issues going back and forth too quick between pages
            await TelerikTheming.PrefetchAsync((p, t) =>
            {
                progress = 100.0 * (double)p / ((double)t);
                this.OnPropertyChanged("Progress");
            });

            prefetched = true;
            this.OnPropertyChanged("Prefetched");
            this.OnPropertyChanged("Loading");
        }
    }

    // TODO: Delete the field mergedDictionaries once we apply the theme on an App level.
    public ICollection<ResourceDictionary> MergedDictionaries
    {
        get => this.mergedDictionaries;
        set
        {
            if (this.UpdateValue(ref this.mergedDictionaries, value) && this.mergedDictionaries != null)
            {
                var theming = this.mergedDictionaries.OfType<TelerikTheming>().SingleOrDefault();
                if (theming == null)
                {
                    this.mergedDictionaries.Add(new TelerikTheming());
                }
            }
        }
    }

    public SwatchDefinition CurrentTheme
    {
        get
        {
            var telerikTheming = this.MergedDictionaries.OfType<TelerikTheming>().Single();

            var swatchDefinition = this.ThemesCatalog.Single(catalogTheme =>
                catalogTheme.Theme == telerikTheming.Theme.Theme &&
                catalogTheme.Swatch == telerikTheming.Theme.Swatch);

            return swatchDefinition;
        }

        set
        {
            var currentTheme = this.CurrentTheme;
            if (value == null || value == currentTheme)
            {
                return;
            }

            var telerikTheming = this.MergedDictionaries.OfType<TelerikTheming>().Single();

            // TODO: Delete this logic once theming is applied on app level.
            // Now it is needed for desktop because there are controls that require the swatches to be merged on app level.
            var appDictionaries = App.Current.Resources.MergedDictionaries;
            appDictionaries.Remove(defaultSwatch);

#if MACCATALYST || WINDOWS
            if (currentTheme.Theme != "Default")
            {
                appDictionaries.Remove(telerikTheming.MergedDictionaries.ElementAt(0));
            }
#endif

            var themeKey = TelerikTheming.Themes.Single(themeKey =>
                themeKey.Theme == value.Theme &&
                themeKey.Swatch == value.Swatch);

            telerikTheming.Theme = themeKey;

            if (value.Theme == "Default")
            {
                appDictionaries.Add(defaultSwatch);
            }
#if MACCATALYST || WINDOWS
            else
            {
                appDictionaries.Add(telerikTheming.MergedDictionaries.ElementAt(0));
                
            }

            this.ResetExampleIfNeeded();
#endif
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
            switch (Application.Current.UserAppTheme)
            {
                case AppTheme.Light: return "Light";
                case AppTheme.Dark: return "Dark";
                default: return "Auto";
            }
        }

        set
        {
            switch (value)
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

    public PageViewModel ParentViewModel { get; internal set; }

    public static string AppThemeToString(AppTheme theme)
    {
        switch (theme)
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

    internal void ResetExampleIfNeeded()
    {
        if (this.CurrentTheme.Theme != "Default")
        {
            return;
        }

        // This triggers recreation of the Example view when the user switches back to Platform.
        // Should be removed once we implement actual Platform theme.
        if (this.ParentViewModel is ExampleViewModel)
        {
            this.ParentViewModel.RaisePropertyChanged("Example");
        }
        else if (this.ParentViewModel is ControlViewModel)
        {
            this.ParentViewModel.RaisePropertyChanged("SelectedExample");
        }
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
