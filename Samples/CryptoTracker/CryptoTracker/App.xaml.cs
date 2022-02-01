using CryptoTracker.Data;
using CryptoTracker.ViewModels;
using CryptoTracker.Pages;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Graphics;
using Application = Microsoft.Maui.Controls.Application;

namespace CryptoTracker
{
	public partial class App : Application
	{
		public App()
		{
			InitializeComponent();
			DependencyService.Register<ICoinDataService, CoinDataService>();
			// We use preprocessor directives in order to apply platform specific implementation.

#if MACCATALYST
			MainPage = new DesktopPage();
#elif WINDOWS
			MainPage = new NavigationPage(new DesktopPage())
			{
				BarBackgroundColor = Colors.White,
				BarTextColor = Color.FromArgb("#121212"),
			};
#else
			MainPage = new NavigationPage(new CoinSelectionPage())
			{
#if IOS
			BarBackgroundColor = Colors.White,
			BarTextColor = Color.FromArgb("#121212"),
#else
			BarBackgroundColor = Color.FromArgb("#121212"),
			BarTextColor = Colors.White,
#endif
			};
#endif
		}
	}
}
