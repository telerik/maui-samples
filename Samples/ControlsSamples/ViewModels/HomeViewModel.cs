using Microsoft.Maui.ApplicationModel;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Devices;
using QSF.Common;
using QSF.Services;
using QSF.Views;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;

namespace QSF.ViewModels;

public class HomeViewModel : PageViewModel
{
    private Control selectedControl;
    private bool isHomeSelected;
    private bool isSearchSelected;
    private bool isSettingsSelected;
    private HighlightedSearchResult selectedSearchResult;

    public HomeViewModel()
    {
        IConfigurationService configurationService = DependencyService.Get<IConfigurationService>();
        Configuration configuration = configurationService.Configuration;

        this.Controls = GetControls(configuration);
        this.HighlightControls = GetHighlightControls(configuration);
        this.Examples = GetExamples(configuration);
        this.MauiHighlights = GetMauiHighlights(configuration);
        this.DemoApps = GetDemoApps(configuration);

        this.SelectHomeCommand = new Command(this.SelectHome);
        this.SelectControlCommand = new Command(this.SelectControl);
        this.SelectDemoAppCommand = new Command(this.SelectDemoApp);
        this.SelectMauiHighlightCommand = new Command(this.SelectMauiHighlight);
        this.SelectSearchCommand = new Command(this.SelectSearch);
        this.SelectSettingsCommand = new Command(this.SelectSettings);

        this.isHomeSelected = true;
    }

    public ObservableCollection<Control> Controls { get; }

    public ObservableCollection<HighlightedControl> HighlightControls { get; }

    public ObservableCollection<Example> Examples { get; }

    public ObservableCollection<MauiHighlight> MauiHighlights { get; }

    public ObservableCollection<DemoApp> DemoApps { get; }

    public ICommand SelectHomeCommand { get; }

    public ICommand SelectControlCommand { get; }

    public ICommand SelectDemoAppCommand { get; }
    
    public ICommand SelectMauiHighlightCommand { get; }

    public ICommand SelectSearchCommand { get; }

    public ICommand SelectSettingsCommand { get; }

    public ICommand LinkTapCommand => new Command<string>(async (url) => await Launcher.OpenAsync(url));

    public Control SelectedControl
    {
        get
        {
            return this.selectedControl;
        }
        set
        {
            if (this.UpdateValue(ref this.selectedControl, value))
            {
                this.OnSelectedControlChanged();
            }
        }
    }

    public bool IsHomeSelected
    {
        get
        {
            return this.isHomeSelected;
        }
        set
        {
            if (this.UpdateValue(ref this.isHomeSelected, value))
            {
                this.OnIsHomeSelectedChanged();
            }
        }
    }

    public bool IsSearchSelected
    {
        get
        {
            return this.isSearchSelected;
        }
        set
        {
            if (this.UpdateValue(ref this.isSearchSelected, value))
            {
                this.OnIsSearchSelectedChanged();
            }
        }
    }

    public bool IsSettingsSelected
    {
        get
        {
            return this.isSettingsSelected;
        }
        set
        {
            if (this.UpdateValue(ref this.isSettingsSelected, value))
            {
                this.OnIsSettingsSelectedChanged();
            }
        }
    }

    public HighlightedSearchResult SelectedSearchResult
    {
        get
        {
            return this.selectedSearchResult;
        }
        set
        {
            if (this.UpdateValue(ref this.selectedSearchResult, value))
            {
                this.OnSelectedSearchResultChanged();
            }
        }
    }

    private static ObservableCollection<Control> GetControls(Configuration configuration)
    {
        ObservableCollection<Control> result = new ObservableCollection<Control>(configuration.Controls);
        return result;
    }

    private static ObservableCollection<HighlightedControl> GetHighlightControls(Configuration configuration)
    {
        ObservableCollection<HighlightedControl> result = new ObservableCollection<HighlightedControl>();

        foreach (HighlightedControl highlighted in configuration.HighlightedControls)
        {
            result.Add(highlighted);

            Control control = configuration.Controls.FirstOrDefault(c => object.Equals(highlighted.DisplayName, c.DisplayName));
            if (control == null)
            {
                continue;
            }

            if (string.IsNullOrEmpty(highlighted.ShortDescription))
            {
                highlighted.ShortDescription = control.ShortDescription;
            }

            if (string.IsNullOrEmpty(highlighted.Icon))
            {
                highlighted.Icon = control.Icon;
            }
        }

        return result;
    }

    private static ObservableCollection<Example> GetExamples(Configuration configuration)
    {
        ObservableCollection<Example> result = new ObservableCollection<Example>();

        foreach (Control control in configuration.Controls)
        {
            foreach (Example example in control.Examples)
            {
                example.ControlName = control.Name;
                result.Add(example);
            }
        }

        return result;
    }

    private static ObservableCollection<MauiHighlight> GetMauiHighlights(Configuration configuration)
    {
        ObservableCollection<MauiHighlight> result = new ObservableCollection<MauiHighlight>(configuration.MauiHighlights);
        return result;
    }

    private static ObservableCollection<DemoApp> GetDemoApps(Configuration configuration)
    {
        ObservableCollection<DemoApp> result = new ObservableCollection<DemoApp>(configuration.DemoApps);
        return result;
    }

    private void SelectHome()
    {
        this.IsHomeSelected = true;
    }

    private void SelectControl(object obj)
    {
        Control control = this.TryGetControl(obj);
        this.SelectedControl = control;

        if (DeviceInfo.Idiom != DeviceIdiom.Desktop)
        {
            this.NavigateTo(this.selectedControl);
        }
    }

    private void SelectDemoApp(object obj)
    {
        string url = this.TryGetUrl(obj);
        TryNavigateToUrl(url);
    }

    private void SelectMauiHighlight(object obj)
    {
        string url = this.TryGetUrl(obj);
        TryNavigateToUrl(url);
    }

    private void SelectSearch()
    {
        this.IsSearchSelected = true;
    }

    private void SelectSettings()
    {
        this.IsSettingsSelected = true;
    }

    private Control TryGetControl(object obj)
    {
        Control control = obj as Control;

        if (control == null && obj is HighlightedControl highlight)
        {
            control = this.Controls.FirstOrDefault(c => c.DisplayName == highlight.DisplayName);
        }

        return control;
    }

    private string TryGetUrl(object obj)
    {
        if (obj is DemoApp demoApp)
        {
            return demoApp.Url;
        }
        else if (obj is MauiHighlight mauiHighlight)
        {
            return mauiHighlight.Url;
        }
        else
        {
            return null;
        }
    }

    private void OnSelectedControlChanged()
    {
        if (this.selectedControl != null)
        {
            this.IsHomeSelected = false;
            this.IsSearchSelected = false;
            this.IsSettingsSelected = false;
        }
    }

    private void NavigateTo(Control control)
    {
        if (control == null)
        {
            return;
        }

        if (control.Examples.Count == 1)
        {
            this.NavigationService.NavigateToExampleAsync(control.Examples[0]);
        }
        else
        {
            this.NavigationService.NavigateToAsync<ControlViewModel>(control);
        }
    }

    public void NavigateToSettings()
    {
        this.NavigationService.NavigateToSettingsPageAsync(new SettingsViewModel());
    }

    public void NavigateToSearch()
    {
        this.NavigationService.NavigateToAsync<SearchViewModelMobile>();
    }

    private void OnIsHomeSelectedChanged()
    {
        if (this.IsHomeSelected)
        {
            this.SelectedControl = null;
            this.IsSearchSelected = false;
            this.IsSettingsSelected = false;
        }
    }

    private void OnIsSearchSelectedChanged()
    {
        if (this.IsSearchSelected)
        {
            this.SelectedControl = null;
            this.IsHomeSelected = false;
            this.IsSettingsSelected = false;
        }
    }

    private void OnIsSettingsSelectedChanged()
    {
        if (this.IsSettingsSelected)
        {
            this.SelectedControl = null;
            this.IsHomeSelected = false;
            this.IsSearchSelected = false;
        }
    }

    private void OnSelectedSearchResultChanged()
    {
        HighlightedSearchResult searchResult = this.selectedSearchResult;
        if (searchResult == null)
        {
            return;
        }

        Control control = this.Controls.FirstOrDefault(c => c.Name == searchResult.ControlName);
        if (control == null)
        {
            return;
        }

        control.StartupExample = control.Examples.FirstOrDefault(e => e.Name == searchResult.ExampleName);
        this.SelectedControl = control;
    }
}
