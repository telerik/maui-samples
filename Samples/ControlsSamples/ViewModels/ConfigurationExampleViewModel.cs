using Microsoft.Maui.Controls;
using System.Windows.Input;

namespace QSF.ViewModels;

public class ConfigurationExampleViewModel : ExampleViewModel
{
    public ConfigurationExampleViewModel()
    {
        this.NavigateToConfigurationPage = new Command(this.NavigateToConfigurationPageExecute);
    }

    public ICommand NavigateToConfigurationPage { get; }

    private void NavigateToConfigurationPageExecute()
    {
        this.NavigationService.NavigateToConfigurationPageAsync(this);
    }
}
