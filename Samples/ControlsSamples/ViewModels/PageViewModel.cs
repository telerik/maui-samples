using Microsoft.Maui.ApplicationModel;
using Microsoft.Maui.Controls;
using QSF.Common;
using QSF.Helpers;
using QSF.Services;
using System.Windows.Input;
using Telerik.AppUtils.Services;

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
            PreferredToolbarColor = App.ApplicationAccentColor
        };

        this.configurationService = DependencyService.Get<IConfigurationService>();

        this.AppTitle = configurationService.Configuration.HeaderTitle;
        this.NavigateToDocumentationCommand = new Command(this.NavigateToDocumentation);
        this.NavigateToFeedbackPortalCommand = new Command(this.NavigateToFeedbackPortal);
        this.NavigateToDownloadTrialCommand = new Command(this.NavigateToDownloadTrial);
        this.NavigateToControlExamplesCodeCommand = new Command(this.NavigateToControlExamplesCode);
        this.NavigateToExampleCodeCommand = new Command(this.NavigateToExampleCode);
        this.NavigateToDescriptionCommand = new Command(this.NavigateToDescription);
        this.NavigateToThemeSettingsCommand = new Command(this.NavigateToThemeSettings);
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

    public ICommand NavigateToControlExamplesCodeCommand { get; private set; }

    public ICommand NavigateToExampleCodeCommand { get; private set; }

    public ICommand NavigateToDescriptionCommand { get; private set; }

    public ICommand NavigateToThemeSettingsCommand { get; private set; }

    public bool IsThemeSettingsVisible => DependencyService.Get<ITestingService>().IsAppUnderTest;

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

    internal void RaisePropertyChanged(string propertyName)
    {
        this.OnPropertyChanged(propertyName);
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

    private void NavigateToControlExamplesCode(object obj)
    {
        Control control = (Control)obj;
        string url = Utils.GetControlExamplesCodeURL(control) ?? this.configurationService.Configuration.ExampleCodeUrl;
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
            descriptionViewModel = new DescriptionViewModel(example);
        }
        else
        {
            Control control = (Control)obj;
            descriptionViewModel = new DescriptionViewModel(control);
        }

        this.NavigationService.NavigateToDescriptionPageAsync(descriptionViewModel);
    }

    private void NavigateToThemeSettings(object obj)
    {
        var examplePage = obj as Pages.ExamplePage;
        ThemeSettingsViewModel viewmodel = new ThemeSettingsViewModel()
        {
            MergedDictionaries = examplePage.Resources.MergedDictionaries,
            ParentViewModel = (ExampleViewModel)examplePage.BindingContext
        };

        this.NavigationService.NavigateToThemeSettingsPageAsync(viewmodel);
    }
}
