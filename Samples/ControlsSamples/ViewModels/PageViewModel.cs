using Microsoft.Maui.ApplicationModel;
using Microsoft.Maui.Controls;
using QSF.Common;
using QSF.Helpers;
using QSF.Services;
using System.Windows.Input;

namespace QSF.ViewModels;

public abstract class PageViewModel : ViewModelBase
{
    private static BrowserLaunchOptions browserLaunchOptions;

    private string appTitle;
    private string headerTitle;
    private string headerDescription;
    private string headerIcon;
    private IConfigurationService configurationService;

    public PageViewModel()
    {
        browserLaunchOptions = new BrowserLaunchOptions()
        {
            TitleMode = BrowserTitleMode.Show,
            PreferredControlColor = Microsoft.Maui.Graphics.Colors.White,
            PreferredToolbarColor = App.Current.Resources["ApplicationAccentColor"] as Microsoft.Maui.Graphics.Color
        };

        this.configurationService = DependencyService.Get<IConfigurationService>();

        this.AppTitle = configurationService.Configuration.HeaderTitle;
        this.NavigateToDocumentationCommand = new Command(this.NavigateToDocumentation);
        this.NavigateToFeedbackPortalCommand = new Command(this.NavigateToFeedbackPortal);
        this.NavigateToDownloadTrialCommand = new Command(this.NavigateToDownloadTrial);
        this.NavigateToExampleCodeCommand = new Command(this.NavigateToExampleCode);
        this.NavigateToDescriptionCommand = new Command(this.NavigateToDescription);
    }

    public string AppTitle
    {
        get
        {
            return this.appTitle;
        }
        protected set
        {
            this.UpdateValue(ref this.appTitle, value);
        }
    }

    public string HeaderTitle
    {
        get
        {
            return this.headerTitle;
        }
        set
        {
            this.UpdateValue(ref this.headerTitle, value);
        }
    }

    public string HeaderDescription
    {
        get
        {
            return this.headerDescription;
        }
        protected set
        {
            this.UpdateValue(ref this.headerDescription, value);
        }
    }

    public string HeaderIcon
    {
        get
        {
            return this.headerIcon;
        }
        protected set
        {
            this.UpdateValue(ref this.headerIcon, value);
        }
    }

    public ICommand NavigateBackCommand => new Command(() => this.NavigationService.NavigateBackAsync());

    public ICommand NavigateToDocumentationCommand { get; private set; }

    public ICommand NavigateToFeedbackPortalCommand { get; private set; }

    public ICommand NavigateToDownloadTrialCommand { get; private set; }

    public ICommand NavigateToExampleCodeCommand { get; private set; }

    public ICommand NavigateToDescriptionCommand { get; private set; }

    public static void TryNavigateToUrl(string url)
    {
        if (url == null)
        {
            return;
        }

        Browser.Default.OpenAsync(new System.Uri(url), browserLaunchOptions);
    }

    public static void TryNavigateToUrl(string url, ICommand fallbackCommand)
    {
        if (!string.IsNullOrEmpty(url))
        {
            TryNavigateToUrl(url);
        }
        else
        {
            fallbackCommand?.Execute(null);
        }
    }

    private void NavigateToDocumentation()
    {
        TryNavigateToUrl(this.configurationService.Configuration.DocumentationUrl);
    }

    private void NavigateToFeedbackPortal()
    {
        string url = this.configurationService.Configuration.FeedbackPortalUrl;
        TryNavigateToUrl(url);
    }

    private void NavigateToDownloadTrial()
    {
        string url = this.configurationService.Configuration.DownloadTrialUrl;
        TryNavigateToUrl(url);
    }

    private void NavigateToExampleCode(object obj)
    {
        Example example = (Example)obj;
        string url = Utils.GetExampleCodeURL(example) ?? this.configurationService.Configuration.ExampleCodeUrl; 
        TryNavigateToUrl(url);
    }

    private void NavigateToDescription(object obj)
    {
        DescriptionViewModel descriptionViewModel;
        Example example = obj as Example;
        if (example != null)
        {
            descriptionViewModel = new DescriptionViewModel(example.Description, example.DisplayName, true);
        }
        else
        {
            Control control = (Control)obj;
            descriptionViewModel = new DescriptionViewModel(control.FullDescription, control.DisplayName);
        }

        this.NavigationService.NavigateToDescriptionPageAsync(descriptionViewModel);
    }
}
