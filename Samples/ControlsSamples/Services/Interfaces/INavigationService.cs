using QSF.Common;
using QSF.ViewModels;
using System.Threading.Tasks;

namespace QSF.Services;

public interface INavigationService
{
    public Task NavigateToAsync<TViewModel>(params object[] arguments);

    public Task NavigateToExampleAsync(Example example);

    public Task NavigateToConfigurationPageAsync(ExampleViewModel viewmodel);

    public Task NavigateToDescriptionPageAsync(DescriptionViewModel viewmodel);

    public Task NavigateToSettingsPageAsync(SettingsViewModel viewmodel);

    public Task NavigateToRootAsync();

    public Task NavigateBackAsync();
}
