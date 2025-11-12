using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Maui.Devices;
using QSF.Services;

#if ANDROID
using Android.Runtime;
#endif

#if WINDOWS
using Microsoft.UI.Xaml;
#endif

#if IOS || MACCATALYST
using ObjCRuntime;
#endif

namespace QSF.Helpers;

public sealed class CrashReporter : IDisposable
{
    private readonly TelemetryService telemetry;
    private bool reporterIsRunning;

    public CrashReporter(TelemetryService telemetryService)
    {
        this.telemetry = telemetryService;
    }

    public void Start()
    {
        if (this.reporterIsRunning)
        {
            return;
        }

        this.reporterIsRunning = true;

        // 1) CLR-wide hooks (cross-platform)
        AppDomain.CurrentDomain.UnhandledException += this.OnUnhandledException;
        TaskScheduler.UnobservedTaskException += this.OnUnobservedTaskException;

        // 2) Platform-specific hooks
#if ANDROID
            AndroidEnvironment.UnhandledExceptionRaiser += this.OnAndroidUnhandled;
#elif WINDOWS
            // WinUI/XAML UI-thread exceptions
            Application.Current.UnhandledException += this.OnWinUIUnhandled;
#elif IOS || MACCATALYST
            // Ensure managed exceptions across Obj-C ↔ .NET boundaries are unwinded so we can log them
            Runtime.MarshalManagedException += (_, args) =>
            {
                args.ExceptionMode = MarshalManagedExceptionMode.UnwindNativeCode;
            };
#endif
    }

    public void Stop() => Dispose();

    public void Dispose()
    {
        if (!this.reporterIsRunning)
        {
            return;
        }

        this.reporterIsRunning = false;

        AppDomain.CurrentDomain.UnhandledException -= this.OnUnhandledException;
        TaskScheduler.UnobservedTaskException -= this.OnUnobservedTaskException;
#if ANDROID
            AndroidEnvironment.UnhandledExceptionRaiser -= this.OnAndroidUnhandled;
#elif WINDOWS
            Application.Current.UnhandledException -= this.OnWinUIUnhandled;
#endif
    }

    private void OnUnhandledException(object? sender, System.UnhandledExceptionEventArgs e)
    {
        if (e.ExceptionObject is Exception ex)
        {
            this.telemetry.TrackException(ex);
        }

        // NOTE: App will typically terminate after this event; use to log only.
        this.telemetry.Flush();
    }

    private void OnUnobservedTaskException(object? sender, UnobservedTaskExceptionEventArgs e)
    {
        this.telemetry.TrackException(e.Exception);
        // Only set Observed if you truly handled the error and it's safe to continue.
        e.SetObserved();
    }

#if ANDROID
        private void OnAndroidUnhandled(object? sender, RaiseThrowableEventArgs e)
        {
            this.telemetry.TrackException(e.Exception);
            // Do not mark handled; just log. Let the OS/runtime decide termination.
            this.telemetry.Flush();
        }
#elif WINDOWS
        private void OnWinUIUnhandled(object? sender, Microsoft.UI.Xaml.UnhandledExceptionEventArgs e)
        {
            this.telemetry.TrackException(e.Exception);
            this.telemetry.Flush();
        }
#endif
}