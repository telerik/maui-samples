using CryptoTracker.Data;
using CryptoTracker.Pages;
using Microsoft.Maui;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Graphics;
using Application = Microsoft.Maui.Controls.Application;

namespace CryptoTracker;

public partial class App : Application
{
	private Page rootPage;
	private Color barBackgroundColor = Color.FromArgb("#121212");
	private Color barTextColor = Color.FromArgb("#FFFFFF");

	public App()
	{
		this.UserAppTheme = Microsoft.Maui.ApplicationModel.AppTheme.Light;
		Application.AccentColor = Color.FromArgb("#045DEA");
		InitializeComponent();
		DependencyService.Register<ICoinDataService, CoinDataService>();
		// We use preprocessor directives in order to apply platform specific implementation.

#if ANDROID || IOS
		this.rootPage = new CoinSelectionPage();
#else
		this.rootPage = new DesktopPage();
#endif

#if MACCATALYST
		MainPage = this.rootPage;
#else
		MainPage = new NavigationPage(this.rootPage)
		{
			BarBackgroundColor = barBackgroundColor,
			BarTextColor = barTextColor
		};
#endif
	}

#if NET7_0_OR_GREATER && (WINDOWS || MACCATALYST)
		protected override Window CreateWindow(IActivationState activationState)
		{
			var window = base.CreateWindow(activationState);
			if (window != null)
			{
				window.MinimumWidth = 1024;
				window.MinimumHeight = 768;
			}

			return window;
		}
#endif
}
