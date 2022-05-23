using System.Linq;
using Microsoft.Maui.Controls;
using SDKBrowserMaui.Common;
using SDKBrowserMaui.ViewModels;

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

        public string NavigateToExample(string controlName, string exampleName)
        {
            var target = this.configurationService.Configuration.Controls
                .Where(control => control.Name == controlName)
                .SelectMany(control => control.Categories)
                .SelectMany(category => category.Examples)
                .First(example => example.Page == exampleName);

            this.NavigateToExample(target);

            return target.Title;
        }

        public bool TryNavigateToExample(string controlName, string exampleName)
        {
            var target = this.configurationService.Configuration.Controls
                .Where(control => control.Name == controlName)
                .SelectMany(control => control.Categories)
                .SelectMany(category => category.Examples)
                .FirstOrDefault(example => example.Page == exampleName || example.Name == exampleName);

            if (target != null)
            {
                this.NavigateToExample(target);
                return true;
            }

            return false;
        }

        private async void NavigateToExample(Example example)
        {
            await this.navigationService.NavigateToAsync<ExampleViewModel>(example);
        }
    }
}
