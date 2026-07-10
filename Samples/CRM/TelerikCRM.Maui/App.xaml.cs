using System.Globalization;
using TelerikCRM.Maui.Common;

namespace TelerikCRM.Maui;

public partial class App : Application
{
    public App()
    {
        this.SetDefaultCulture();
        this.UserAppTheme = Microsoft.Maui.ApplicationModel.AppTheme.Light;

        InitializeComponent();

        if (string.IsNullOrEmpty(ServiceConstants.ServiceUrl))
        {
            throw new NotImplementedException("You need to update TelerikCRM.Maui/Common/ServiceConstants.cs with your Datasync server's URL.");
        }
    }

    protected override Window CreateWindow(IActivationState activationState)
    {
#if WINDOWS || MACCATALYST
        var window = new Window(new NavigationPage(new Views.Desktop.MainPage()));
        window.MinimumWidth = 1280;
        window.MinimumHeight = 768;
        window.Title = "Telerik CRM";
#else
        var window = new Window(new NavigationPage(new Views.Mobile.MainPage()));
#endif
        return window;
    }
    
    private void SetDefaultCulture()
    {
        var culture = CultureInfo.GetCultureInfo("en-US");
        CultureInfo.DefaultThreadCurrentCulture = culture;
        CultureInfo.DefaultThreadCurrentUICulture = culture;
        Thread.CurrentThread.CurrentCulture = culture;
        Thread.CurrentThread.CurrentUICulture = culture;
    }
}