using QSF.Common;
using QSF.ViewModels;
using System.Threading.Tasks;

namespace QSF.Services;

public interface INavigationService
{
    public Task NavigateToAsync<TViewModel>(params object[] arguments);

    public Task NavigateToExampleAsync(Example example, bool popToMain = false, bool? animated = null);

    public Task NavigateToConfigurationPageAsync(ExampleViewModel viewmodel);

    public Task NavigateToDescriptionPageAsync(DescriptionViewModel viewmodel);

    public Task NavigateToSettingsPageAsync(SettingsViewModel viewmodel);

    public Task NavigateToThemeSettingsPageAsync(ThemingViewModel viewmodel);

    public Task NavigateToRootAsync();

    public Task NavigateBackAsync();

    public Task NavigateCommand(string cmd);
}
