using Foundation;
using Microsoft.Maui;
using Microsoft.Maui.Hosting;

namespace CryptoTracker
{
	[Register("AppDelegate")]
	public class AppDelegate : MauiUIApplicationDelegate
	{
		protected override MauiApp CreateMauiApp() => MauiProgram.CreateMauiApp();
	}
}