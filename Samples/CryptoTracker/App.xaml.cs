using CryptoTracker.Data;
using CryptoTracker.Pages;
using Microsoft.Maui;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Graphics;
using Application = Microsoft.Maui.Controls.Application;

namespace CryptoTracker;

public partial class App : Application
{
    public App()
    {
        this.UserAppTheme = Microsoft.Maui.ApplicationModel.AppTheme.Light;
        Application.AccentColor = Color.FromArgb("#045DEA");

        this.InitializeComponent();

        DependencyService.Register<ICoinDataService, CoinDataService>();
    }

    protected override Window CreateWindow(IActivationState activationState)
    {
#if ANDROID || IOS
        var rootPage = new CoinSelectionPage();
#else
        var rootPage = new DesktopPage();
#endif

#if MACCATALYST
        var window = new Window(rootPage);
#else
        var window = new Window(new NavigationPage(rootPage)
        {
            BarBackgroundColor = Color.FromArgb("#121212"),
            BarTextColor = Color.FromArgb("#FFFFFF")
        });
#endif

#if WINDOWS || MACCATALYST
        window.MinimumWidth = 1024;
        window.MinimumHeight = 768;
#endif
        return window;
    }
}
