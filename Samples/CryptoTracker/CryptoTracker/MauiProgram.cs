using Microsoft.Maui.Hosting;
using Microsoft.Maui.Controls.Hosting;
using Telerik.Maui.Controls.Compatibility;

namespace CryptoTracker
{
	public static class MauiProgram
	{
		public static MauiApp CreateMauiApp()
		{
			var builder = MauiApp.CreateBuilder();
			builder
				.UseMauiApp<App>()
				.UseTelerik()
				.ConfigureFonts(fonts =>
				{
					fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
					fonts.AddFont("Ubuntu-MediumItalic.ttf", "UbuntuMediumItalic");
					fonts.AddFont("telerikfontexamples.ttf", "telerikFontExamples");
				});

			return builder.Build();
		}
	}
}