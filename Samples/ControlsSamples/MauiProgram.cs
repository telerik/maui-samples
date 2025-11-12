using Microsoft.ApplicationInsights.Extensibility;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Maui.Controls.Hosting;
using Microsoft.Maui.Hosting;
using Microsoft.Maui.Storage;
using QSF.Helpers;
using QSF.Services;
using System;
using Telerik.AppUtils.Services;
using Telerik.Maui.Controls.Compatibility;


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
                .UseTelerikInHouseTestingService(
                    // Set this to true, if you want to test the demos with pseudorandom data instead of random data
                    defaultIsAppUnderTest: false,
                    // Set to 6364 to open testing port, used to take appium-free screenshots of examples
                    testCommandTcpPort: null
                )
                .ConfigureMauiHandlers((handlers) =>
                {
#if WINDOWS
                    // The default look and feel of ImageButton was changed with preview 11.
                    // The custom RadImageButtonHandler brings back the old look from preview 10.
                    // Remove or improve that implementation if the new look of ImageButton is needed in WinUI.
                    handlers.AddHandler(typeof(Microsoft.Maui.Controls.ImageButton), typeof(QSF.WinUI.RadImageButtonHandler));
#endif
                })
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("telerikcontrolsicons.ttf", "TelerikControlsIcons");
                    fonts.AddFont("telerikfontexamples.ttf", "TelerikFontExamples");
                });

            var connectionString = TelemetryService.GetConnectionString();
            var shouldStartTracking = !string.IsNullOrEmpty(connectionString);
            if (shouldStartTracking)
            {
                ConfigureApplicationInsights(builder, connectionString);
            }

            var app = builder.Build();

            if (shouldStartTracking)
            {
                // Start as early as possible (after container is built).
                app.Services.GetRequiredService<CrashReporter>().Start();
            }

            return app;
        }

        private static void ConfigureApplicationInsights(MauiAppBuilder builder, string connectionString)
        {
            var telemetryConfiguration = new TelemetryConfiguration();
            telemetryConfiguration.ConnectionString = connectionString;
            telemetryConfiguration.TelemetryInitializers.Add(new ApplicationTelemetryInitializer());

            builder.Logging.AddApplicationInsights(
                configureTelemetryConfiguration: (config) =>
                {
                    config.ConnectionString = connectionString;
                    config.TelemetryInitializers.Add(new ApplicationTelemetryInitializer());
                },
                configureApplicationInsightsLoggerOptions: _ => { }
            );

            builder.Services.AddSingleton(telemetryConfiguration);
            builder.Services.AddSingleton<TelemetryService>();
            builder.Services.AddSingleton<CrashReporter>();
        }
    }
}