using System.Windows.Input;
using Microsoft.Maui.ApplicationModel;
using Microsoft.Maui.Controls;
using QSF.Common;
using QSF.Helpers;
using QSF.Services;

namespace QSF.ViewModels;

public class DescriptionViewModel : ViewModelBase
{
    private const string ExampleHeaderLabelText = "Example Info";
    private const string ControlHeaderLabelText = "Control Info";

    public DescriptionViewModel(Control control)
    {
        this.Description = control.FullDescription;
        this.DescriptionLabel = control.DisplayName;
        this.HeaderLabel = ControlHeaderLabelText;

        this.NavigateCommandParameter = control;
        this.NavigateToGitHubCodeCommand = new Command(this.NavigateToGitHubCode);
    }

    public DescriptionViewModel(Example example)
    {
        this.Description = example.Description;
        this.DescriptionLabel = example.DisplayName;
        this.HeaderLabel = ExampleHeaderLabelText;

        this.NavigateCommandParameter = example;
        this.NavigateToGitHubCodeCommand = new Command(this.NavigateToGitHubCode);
    }

    public string HeaderLabel { get; private set; }

    public string DescriptionLabel { get; private set; }

    public string Description { get; private set; }

    public object NavigateCommandParameter { get; private set; }

    public ICommand NavigateToGitHubCodeCommand { get; private set; }

    private void NavigateToGitHubCode(object obj)
    {
        var configurationService = DependencyService.Get<IConfigurationService>();
        string url = configurationService?.Configuration.ExampleCodeUrl; 

        if (obj is Control control)
        {
            url = Utils.GetControlExamplesCodeURL(control) ?? configurationService?.Configuration.ExampleCodeUrl; 
        }
        else if (obj is Example example)
        {
            url = Utils.GetExampleCodeURL(example) ?? configurationService?.Configuration.ExampleCodeUrl; 
        }

        if (url != null)
        {
            Browser.Default.OpenAsync(new System.Uri(url), new BrowserLaunchOptions()
            {
                TitleMode = BrowserTitleMode.Show,
                PreferredControlColor = Microsoft.Maui.Graphics.Colors.White,
                PreferredToolbarColor = App.ApplicationAccentColor
            });
        }
    }
}
