using Microsoft.Maui;
using Microsoft.Maui.Hosting;
using Microsoft.Maui.Controls.Compatibility;
using Microsoft.Maui.Controls.Hosting;
using Telerik.Maui.Controls.Compatibility;
using Microsoft.Maui.Controls;

namespace QSF
{
	public static class MauiProgram
	{
		public static MauiApp CreateMauiApp()
		{
			var builder = MauiApp.CreateBuilder();
			builder
				.UseMauiApp<App>()
				.UseTelerik()
				.ConfigureMauiHandlers((handlers) =>
				{
#if WINDOWS
					// The default look and feel of ImageButton was changed with preview 11.
					// The custom RadImageButtonHandler brings back the old look from preview 10.
					// Remove or improve that implementation if the new look of ImageButton is needed in WinUI.
					handlers.AddHandler(typeof(ImageButton), typeof(QSF.WinUI.RadImageButtonHandler));
#endif
				})
				.ConfigureFonts(fonts =>
				{
					fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
					fonts.AddFont("telerikcontrolsicons.ttf", "TelerikControlsIcons");
					fonts.AddFont("telerikfontexamples.ttf", "TelerikFontExamples");
				});

			return builder.Build();
		}
	}
}