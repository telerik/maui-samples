using Microsoft.ApplicationInsights;
using Microsoft.ApplicationInsights.Channel;
using Microsoft.ApplicationInsights.Extensibility;
using Microsoft.Extensions.Configuration;
using Microsoft.Maui.Storage;
using QSF.Common;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace QSF.Services;

public class TelemetryService
{
    private const string PreferencesTelemetryKey = "IsTelemetryDisabled";
    private readonly TelemetryClient telemetryClient;
    private readonly TelemetryConfiguration configuration;
    private Example currentExample;

    public TelemetryService(TelemetryConfiguration telemetryConfiguration)
    {
        this.telemetryClient = new TelemetryClient(telemetryConfiguration);
        this.configuration = telemetryConfiguration;
        this.IsDisabled = Preferences.Get(PreferencesTelemetryKey, false);
    }

    public bool IsDisabled
    {
        get
        {
            return this.configuration.DisableTelemetry;
        }
        set
        {
            if (this.configuration.DisableTelemetry != value)
            {
                this.configuration.DisableTelemetry = value;
                Preferences.Set(PreferencesTelemetryKey, value);
            }
        }
    }

    public void TrackAppStart()
    {
        var properties = new Dictionary<string, string>
        {
            ["DeviceType"] = Microsoft.Maui.Devices.DeviceInfo.DeviceType.ToString(),
            ["Idiom"] = Microsoft.Maui.Devices.DeviceInfo.Idiom.ToString(),
        };

        var metrics = new Dictionary<string, double>
        {
            ["ScreenWidth"] = Microsoft.Maui.Devices.DeviceDisplay.MainDisplayInfo.Width,
            ["ScreenHeight"] = Microsoft.Maui.Devices.DeviceDisplay.MainDisplayInfo.Height,
            ["Density"] = Microsoft.Maui.Devices.DeviceDisplay.MainDisplayInfo.Density,
            ["ProcessUptimeMs"] = Environment.TickCount64,
            ["ProcessorCount"] = Environment.ProcessorCount,
        };

        this.telemetryClient.TrackEvent("AppStart", properties, metrics);
    }

    public void TrackNavigatedToExample(Example example)
    {
        if (example == null || this.currentExample == example)
        {
            return;
        }

        var properties = new Dictionary<string, string>
        {
            ["ControlName"] = example.ControlName,
            ["ExampleName"] = example.Name,
            ["ExampleFullName"] = $"{example.ControlName}.{example.Name}"
        };

        this.currentExample = example;
        this.telemetryClient.TrackEvent("NavigatedToExample", properties);
    }

    public void TrackThemeChanged(TelerikTheme theme)
    {
        var properties = new Dictionary<string, string>
        {
            ["Theme"] = theme.ToString(),
        };

        this.telemetryClient.TrackEvent("ThemeChanged", properties);
    }

    public void TrackEvent(string eventName)
    {
        this.telemetryClient.TrackEvent(eventName);
    }

    public void TrackEvent(string eventName, Dictionary<string, string> properties)
    {
        this.telemetryClient.TrackEvent(eventName, properties);
    }

    public void TrackEvent(string eventName, Dictionary<string, string> properties, Dictionary<string, double> metrics)
    {
        this.telemetryClient.TrackEvent(eventName, properties, metrics);
    }

    public void TrackMetric(string metricName, double value)
    {
        this.telemetryClient.TrackMetric(metricName, value);
    }

    public void TrackException(Exception exception)
    {
        var properties = new Dictionary<string, string>();
        if (this.currentExample != null)
        {
            properties["ExampleFullName"] = $"{this.currentExample.ControlName}.{this.currentExample.Name}";
        }

        this.telemetryClient.TrackException(exception, properties);
    }

    public void Flush()
    {
        this.telemetryClient.Flush();
    }

    internal static string GetConnectionString()
    {
        try
        {
            using var stream = Microsoft.Maui.Storage.FileSystem
                .OpenAppPackageFileAsync("appsettings.json").GetAwaiter().GetResult();
            if (stream == null)
                return string.Empty;

            using var doc = System.Text.Json.JsonDocument.Parse(stream);
            if (!doc.RootElement.TryGetProperty("ApplicationInsights", out var aiElem))
                return string.Empty;

            if (!aiElem.TryGetProperty("InstrumentationKey", out var keyProp))
                return string.Empty;

            var key = keyProp.GetString();
            if (string.IsNullOrWhiteSpace(key))
                return string.Empty;

            return $"InstrumentationKey={key};IngestionEndpoint=https://eastus-8.in.applicationinsights.azure.com/;LiveEndpoint=https://eastus.livediagnostics.monitor.azure.com/;ApplicationId=33ddb49d-c1a1-4e5a-b0ec-93f81e3b23b4";
        }
        catch
        {
            return string.Empty;
        }
    }
}

public class ApplicationTelemetryInitializer : ITelemetryInitializer
{
    private static readonly Lazy<string> osVersion = new Lazy<string>(GetOsVersion);
    private static readonly Lazy<string> controlsVersion = new Lazy<string>(GetControlsVersion);
    private static readonly Lazy<string> installationId = new Lazy<string>(GetOrCreateInstallationId);
    private static readonly Lazy<string> sessionId = new Lazy<string>(() => Guid.NewGuid().ToString("N"));

    public void Initialize(ITelemetry telemetry)
    {
        telemetry.Context.GlobalProperties["Platform"] = Microsoft.Maui.Devices.DeviceInfo.Platform.ToString();
        telemetry.Context.GlobalProperties["DeviceModel"] = Microsoft.Maui.Devices.DeviceInfo.Model;
        telemetry.Context.GlobalProperties["DeviceManufacturer"] = Microsoft.Maui.Devices.DeviceInfo.Manufacturer;
        telemetry.Context.GlobalProperties["OSVersion"] = osVersion.Value;
        telemetry.Context.GlobalProperties["ControlsVersion"] = controlsVersion.Value;

        // Also populate standard AI fields
        telemetry.Context.Component.Version = Microsoft.Maui.ApplicationModel.AppInfo.VersionString;
        telemetry.Context.Session.Id = sessionId.Value;

        // Runtime
        telemetry.Context.GlobalProperties["Framework"] = System.Runtime.InteropServices.RuntimeInformation.FrameworkDescription;
        telemetry.Context.GlobalProperties["ProcessArch"] = System.Runtime.InteropServices.RuntimeInformation.ProcessArchitecture.ToString();
        telemetry.Context.GlobalProperties["OSArch"] = System.Runtime.InteropServices.RuntimeInformation.OSArchitecture.ToString();

        // Locale/time
        telemetry.Context.GlobalProperties["Culture"] = System.Globalization.CultureInfo.CurrentCulture.Name;
        telemetry.Context.GlobalProperties["UICulture"] = System.Globalization.CultureInfo.CurrentUICulture.Name;
        telemetry.Context.GlobalProperties["TimeZone"] = TimeZoneInfo.Local.Id;

        // UI context
        telemetry.Context.GlobalProperties["OSTheme"] = Microsoft.Maui.Controls.Application.Current?.RequestedTheme.ToString() ?? "Unknown";

        // Install/upgrade state 
        telemetry.Context.GlobalProperties["IsFirstLaunchEver"] = Microsoft.Maui.ApplicationModel.VersionTracking.IsFirstLaunchEver.ToString();
        telemetry.Context.GlobalProperties["IsFirstLaunchForCurrentVersion"] = Microsoft.Maui.ApplicationModel.VersionTracking.IsFirstLaunchForCurrentVersion.ToString();

        // Useful for App Insights filtering
        telemetry.Context.Cloud.RoleName = Microsoft.Maui.ApplicationModel.AppInfo.Name;
        telemetry.Context.Cloud.RoleInstance = installationId.Value;

        // Assign a stable pseudonymous user id so AI can compute unique users.
        telemetry.Context.User.Id = installationId.Value;
        telemetry.Context.User.AccountId = installationId.Value;
    }

    private static string GetOsVersion()
    {
#if MACCATALYST
        return Foundation.NSProcessInfo.ProcessInfo.OperatingSystemVersion.ToString();
#else
        return Microsoft.Maui.Devices.DeviceInfo.VersionString;
#endif
    }

    private static string GetControlsVersion()
    {
        var assembly = typeof(Telerik.Maui.Controls.RadBorder).GetTypeInfo().Assembly;
        var version = assembly.GetName().Version;
        return version != null ? $"{version.Major}.{version.Minor}.{version.Build}" : "Unknown";
    }

    private static string GetOrCreateInstallationId()
    {
        const string key = "Telerik.ControlsSamples.Telemetry.InstallationId";
        var id = Preferences.Get(key, null);
        if (string.IsNullOrEmpty(id))
        {
            id = Guid.NewGuid().ToString("N");
            Preferences.Set(key, id);
        }

        return id;
    }
}