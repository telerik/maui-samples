using System.Linq;
using Microsoft.Maui.Controls;
using SDKBrowserMaui.Common;
using SDKBrowserMaui.ViewModels;
using System.Threading.Tasks;

namespace SDKBrowserMaui.Services
{
    public class BackdoorService : IBackdoorService
    {
        private readonly IConfigurationService configurationService;
        private readonly INavigationService navigationService;

        public BackdoorService()
        {
            this.configurationService = DependencyService.Get<IConfigurationService>();
            this.navigationService = DependencyService.Get<INavigationService>();
        }

        public async Task<string> NavigateToExampleAsync(string controlName, string exampleName)
        {
            var target = this.configurationService.Configuration.Controls
                .Where(control => control.Name == controlName)
                .SelectMany(control => control.Categories)
                .SelectMany(category => category.Examples)
                .First(example => example.Page == exampleName || example.Name == exampleName);

            await this.navigationService.NavigateToExampleAsync<ExampleViewModel>(target, popToMain: true, isAnimated: false);
            return target.Title;
        }

        public async Task NavigateToSearchAsync()
        {
            await this.navigationService.NavigateToAsync<SearchViewModel>();
        }

        public async Task NavigateBackAsync()
        {
            await this.navigationService.NavigateBackAsync(false);
        }

        public bool TrySetTheme(string themeName, string themeSwatch)
        {
            var themeViewModel = DependencyService.Get<ThemeSettingsViewModel>();
            if (themeViewModel != null)
            {
                var definition = themeViewModel.ThemesCatalog.FirstOrDefault(t => t.Theme == themeName && t.Swatch == themeSwatch);
                if (definition != null)
                {
                    themeViewModel.CurrentTheme = definition;
                    return true;
                }
            }
            
            return false;
        }
    }
}
