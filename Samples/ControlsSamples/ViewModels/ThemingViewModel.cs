using Microsoft.Maui.Controls;
using QSF.Styles;
#if WINDOWS
using QSF.WinUI;
#endif
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace QSF.ViewModels
{
    public class ThemingViewModel : ViewModelBase
    {
        private readonly ResourceDictionary dark = new ColorsDark();
        private readonly ResourceDictionary light = new ColorsLight();

        private SwatchDefinition currentTheme;

        private static bool areLightResourcesMerged = true;
        private static bool prefetchStarted = false;
        private static bool prefetched = false;
        private static double progress = 0;
        private static ThemingViewModel instance;
        
        private ThemingViewModel()
        {
            this.HeaderLabel = "Theme Settings";
            string result = null;

            Assembly assembly = Assembly.GetExecutingAssembly();
            using (Stream stream = assembly.GetManifestResourceStream("QSF.ViewModels.Themes.Catalog.json"))
            {
                using (StreamReader reader = new StreamReader(stream))
                {
                    result = reader.ReadToEnd();
                }
            }

            var themeStrings = Enum.GetValues(typeof(TelerikTheme)).Cast<TelerikTheme>()
                .Select(theme => Regex.Replace(theme.ToString(), "(?<!^)([A-Z])", " $1")).ToList();

            var themeDefinitions = JsonSerializer.Deserialize<ThemeDefinition[]>(result);

            this.ThemesList = themeDefinitions
                .SelectMany(themeDef => themeDef.Swatches)
                .Where(swatchDef => themeStrings.Contains(string.IsNullOrEmpty(swatchDef.Swatch) ? swatchDef.Theme : swatchDef.Theme + " " + swatchDef.Swatch))
                .ToList();
        }

        public static ThemingViewModel Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new ThemingViewModel();
                }

                return instance;
            }
        }

        public static bool IsDarkMode
        {
            get
            {
                return !areLightResourcesMerged;
            }
        }

        public Action<SwatchDefinition, SwatchDefinition> ThemeChangedCallback { get; set; }
        public string HeaderLabel { get; private set; }
        public List<SwatchDefinition> ThemesList { get; private set; }
        public bool PrefetchStarted = prefetchStarted;
        public bool Prefetched => prefetched;
        public bool Loading => prefetchStarted && !prefetched;
        public double Progress => progress;

        public SwatchDefinition CurrentTheme
        {
            get
            {
                if (this.currentTheme == null)
                {
                    this.currentTheme = this.ThemesList.FirstOrDefault(t => t.Value == TelerikThemeResources.AppTheme);
                }

                return this.currentTheme;
            }
            set
            {
                if (this.currentTheme != value && value != null)
                {
                    var oldTheme = this.currentTheme;
                    var newTheme = value;
                    this.currentTheme = newTheme;
                    this.OnPropertyChanged();
                    this.TelemetryService.TrackThemeChanged(newTheme.Value);

                    Application.Current.Dispatcher.Dispatch(() =>
                    {
                        if (this.currentTheme.DisplayFullName.Contains("Dark"))
                        {
                            this.MergeDarkResources();
                        }
                        else
                        {
                            this.MergeLightResources();
                        }

                        TelerikThemeResources.AppTheme = this.currentTheme.Value;
                        this.ThemeChangedCallback?.Invoke(oldTheme, newTheme);
#if WINDOWS
                        TelerikTitleBarHelper.SetRequestedTheme(IsDarkMode);
#endif
                    });
                }
            }
        }

        public async Task PrefetchAsync()
        {
            if (!prefetchStarted)
            {
                prefetchStarted = true;
                this.OnPropertyChanged("PrefetchStarted");
                this.OnPropertyChanged("Loading");

                // NOTE: This is instance, while the progress is static...
                // may have minor issues going back and forth too quick between pages
                await TelerikThemeResources.PrefetchAsync((p, t) =>
                {
                    progress = 100.0 * (double)p / ((double)t);
                    this.OnPropertyChanged(nameof(this.Progress));
                });

                prefetched = true;
                this.OnPropertyChanged("Prefetched");
                this.OnPropertyChanged("Loading");
            }
        }

        public void Initialize()
        {
            this.CurrentTheme = this.ThemesList.FirstOrDefault();
        }

        private void MergeDarkResources()
        {
            if (!areLightResourcesMerged)
            {
                return;
            }

            var lightColors = Application.Current.Resources.MergedDictionaries.OfType<ColorsLight>().FirstOrDefault();
            if (lightColors != null)
            {
                Application.Current.Resources.MergedDictionaries.Remove(lightColors);
            }

            Application.Current.UserAppTheme = Microsoft.Maui.ApplicationModel.AppTheme.Dark;
            Application.Current.Resources.MergedDictionaries.Add(dark);
            areLightResourcesMerged = false;
        }

        private void MergeLightResources()
        {
            if (areLightResourcesMerged)
            {
                return;
            }

            var darkColors = Application.Current.Resources.MergedDictionaries.OfType<ColorsDark>().FirstOrDefault();
            if (darkColors != null)
            {
                Application.Current.Resources.MergedDictionaries.Remove(darkColors);
            }

            Application.Current.UserAppTheme = Microsoft.Maui.ApplicationModel.AppTheme.Light;
            Application.Current.Resources.MergedDictionaries.Add(light);
            areLightResourcesMerged = true;
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

        public TelerikTheme Value => (TelerikTheme)Enum.Parse(typeof(TelerikTheme), this.DisplayFullName.Replace(" ", string.Empty));

        [JsonPropertyName("previewColors")]
        public string[] PreviewColors { get; set; }
    }
}
