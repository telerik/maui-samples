using Microsoft.Maui.Controls;
using Microsoft.Maui.Controls.Hosting;
using Microsoft.Maui.Hosting;
using Telerik.Maui.Controls.Compatibility;

namespace SDKBrowserMaui
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
					handlers.AddHandler(typeof(ImageButton), typeof(SDKBrowserMaui.WinUI.RadImageButtonHandler));
#endif
				})
				.ConfigureFonts(fonts =>
				{
					fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
					fonts.AddFont("telerikfontexamples.ttf", "TelerikFontExamples");
				});

			return builder.Build();
		}
	}
}
