using Microsoft.Maui.Controls;
using Microsoft.Maui.Devices;
using QSF.Common;
using QSF.Services;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;

namespace QSF.ViewModels;

public class ControlViewModel : PageViewModel
{
    private readonly Control control;

    private Example selectedExample;

    public ControlViewModel(Control control)
    {
        this.control = control;
        this.HeaderTitle = control.DisplayName;
        this.Examples = new ObservableCollection<Example>();

        foreach (var example in control.Examples)
        {
            // The ControlName could be specified in the config.xml when we want
            // to show example from different control in the current control section.
            if (string.IsNullOrEmpty(example.ControlName))
            {
                example.ControlName = control.Name;
            }

            this.Examples.Add(example);
        }

        this.NavigateToExampleCommand = new Command(obj => this.NavigateTo(obj as Example));
        this.NavigateToControlDocumentationCommand = new Command(this.NavigateToControlDocumentation);
        this.NavigateToControlFeedbackPortalCommand = new Command(this.NavigateToControlFeedbackPortal);

        if (DeviceInfo.Idiom == DeviceIdiom.Desktop)
        {
            // Restore the selection if reloaded while changing theme.
            this.SelectedExample = this.control.SelectedExample ?? this.control.StartupExample ?? this.Examples.FirstOrDefault();
        }

        ThemingViewModel.Instance.ThemeChangedCallback = (oldTheme, newTheme) => this.OnPropertyChanged("SelectedExample");
    }

    public Control Control
    {
        get
        {
            return this.control;
        }
    }


    public Example SelectedExample
    {
        get 
        { 
            return this.selectedExample; 
        }
        set 
        {
            if (this.UpdateValue(ref this.selectedExample, value))
            {
                this.Control.SelectedExample = this.selectedExample;
                this.TelemetryService.TrackNavigatedToExample(this.selectedExample);
            }
        }
    }

    public ObservableCollection<Example> Examples { get; private set; }

    public ICommand NavigateToExampleCommand { get; private set; }

    public ICommand NavigateToControlDocumentationCommand { get; private set; }

    public ICommand NavigateToControlFeedbackPortalCommand { get; private set; }

    public void NavigateTo(Example example)
    {
        if (example == null)
        {
            return;
        }

        this.NavigationService.NavigateToExampleAsync(example);
    }

    private void NavigateToControlDocumentation()
    {
        TryNavigateToUrl(this.GetDocumentationURL(), this.NavigateToDocumentationCommand);
    }

    private void NavigateToControlFeedbackPortal()
    {
        TryNavigateToUrl(this.control.FeedbackPortalUrl, this.NavigateToFeedbackPortalCommand);
    }

    private string GetDocumentationURL()
    {
        if (!string.IsNullOrEmpty(this.control.DocumentationURL))
        {
            return this.control.DocumentationURL;
        }
        else
        {
            IConfigurationService configurationService = DependencyService.Get<IConfigurationService>();
            string url = configurationService.Configuration.DocumentationUrl?.Replace("introduction", $"controls/{this.control.Name.ToLower()}/overview");
            return url;
        }
    }
}
