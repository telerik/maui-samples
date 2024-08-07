using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Maui.LifecycleEvents;
using System.Net;
using System.Net.Sockets;

namespace Telerik.AppUtils.Services;

public static class TestingExtensions
{
    private static TcpListener? tcpCommandServer = null;

    public static MauiAppBuilder UseTelerikInHouseTestingService(this MauiAppBuilder @this, bool defaultIsAppUnderTest = false, int? testCommandTcpPort = null)
    {
        TestingService instance = new TestingService()
        {
            IsAppUnderTest = defaultIsAppUnderTest,
            TestCommandTcpPort = testCommandTcpPort
        };
        
        @this.Services.TryAddSingleton<ITestingService>(instance);
        DependencyService.RegisterSingleton<ITestingService>(instance);

        if (IsEnvTrue("EnableTelerikUIAutomation") ||
            IsEnvTrue("TK_TEST") ||
            IsCommandLineArg("EnableTelerikUIAutomation") ||
            IsCommandLineArg("TK_TEST"))
        {
            instance.IsAppUnderTest = true;
        }

        @this.ConfigureLifecycleEvents(events =>
        {
#if ANDROID
            events.AddAndroid(android =>
            {
                android.OnCreate((activity, bundle) =>
                {
                    if (!instance.IsAppUnderTest)
                    {
                        instance.IsAppUnderTest = activity.Intent?.GetBooleanExtra("TK_TEST", false) == true;
                    }

                    if (instance.IsAppUnderTest)
                    {
                        BootUpCommandServer(instance);
                    }
                });

                android.OnNewIntent((activity, intent) =>
                {
                    if (!instance.IsAppUnderTest && intent != null)
                    {
                        instance.IsAppUnderTest = intent.GetBooleanExtra("TK_TEST", false);
                    }
                });
            });
#elif IOS
            events.AddiOS(ios =>
            {
                ios.FinishedLaunching((app, launchOptions) =>
                {
                    if (IsEnvTrue("EnableTelerikUIAutomation") || IsEnvTrue("TK_TEST"))
                    {
                        UIKit.UIView.AnimationsEnabled = false;
                    }

                    if (instance.IsAppUnderTest)
                    {
                        BootUpCommandServer(instance);
                    }

                    app.Windows.Single().OverrideUserInterfaceStyle = UIKit.UIUserInterfaceStyle.Light;

                    return false;
                });
            });
#endif
        });

        return @this;
    }

    private static void BootUpCommandServer(TestingService testingService)
    {
        int? port = testingService.TestCommandTcpPort;
        try
        {
            var portEnvVar = Environment.GetEnvironmentVariable("TK_TEST_TCP_COMMAND_PORT");
            if (portEnvVar != null)
            {
                port = int.Parse(portEnvVar);
            }
        }
        catch
        {
        }

        if (port != null)
        {
            // Experimental TCP commands for testing
            try
            {
                Console.WriteLine("TEST COMMAND! Starting on port " + port.Value);
                var ipEndPoint = new IPEndPoint(IPAddress.Loopback, port.Value);
                tcpCommandServer = new TcpListener(ipEndPoint);
                tcpCommandServer.Start();
                tcpCommandServer.BeginAcceptTcpClient(BeginAcceptTcpClientAsync, testingService);
                Console.WriteLine("TEST COMMAND! Listening on " + port.Value);
            }
            catch(Exception e)
            {
                Console.WriteLine("TEST COMMAND! Failed to start server.");
                Console.WriteLine(e);
            }
        }
    }

    private static void BeginAcceptTcpClientAsync(IAsyncResult client)
    {
        TestingService testingService = (TestingService)client.AsyncState!;
        Task.Run(async () =>
        {
            var socket = tcpCommandServer!.EndAcceptTcpClient(client);
            using var stream = socket.GetStream();
            using var reader = new StreamReader(stream);
            using var writer = new StreamWriter(stream);
            string? line = null;
            while((line = await reader.ReadLineAsync()) != null)
            {
                Console.WriteLine($"TEST COMMAND> {line}");
                try
                {
                    var result = await testingService.HandleCommandAsync(line);
                    if (!string.IsNullOrWhiteSpace(result))
                    {
                        await writer.WriteLineAsync(result);
                        await writer.FlushAsync();
                    }
                }
                catch(Exception e)
                {
                    await writer.WriteLineAsync($"ERROR: {e.Message}");
                    await writer.FlushAsync();
                }
            }
        });
        tcpCommandServer!.BeginAcceptTcpClient(BeginAcceptTcpClientAsync, testingService);
    }

    private static bool IsEnvTrue(string var)
    {
        string? upper = Environment
            .GetEnvironmentVariable(var)
            ?.ToUpperInvariant();
        return !string.IsNullOrWhiteSpace(upper) && !(upper == "NO" || upper == "FALSE" || upper == "0");
    }

    private static bool IsCommandLineArg(string var)
    {
        return Environment
            .GetCommandLineArgs()
            .Any(arg => arg?.ToUpperInvariant() == var?.ToUpperInvariant());
    }
}