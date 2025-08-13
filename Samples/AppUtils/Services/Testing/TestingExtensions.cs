using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Maui.LifecycleEvents;
using System.Net;
using System.Net.Sockets;

#if ANDROID
using Telerik.Maui.Controls;
using Android.Views;
#endif

namespace Telerik.AppUtils.Services;

public static class TestingExtensions
{
    private static TcpListener? tcpCommandServer = null;
    private static int DefaultTestCommandTcpPort = 6364;

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

        if (instance.IsAppUnderTest)
        {
            SetAutomationIds();
            StopScrollBarsHiding();

#if !ANDROID && !IOS
            BootUpCommandServer(instance);
#endif
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
                        SetAutomationIds();
                        StopScrollBarsHiding();

                        // Workaround for issue #82754
                        StopEntryEmojiCompat();

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

                    if (app.Windows.Count() > 0)
                    {
                        app.Windows.Single().OverrideUserInterfaceStyle = UIKit.UIUserInterfaceStyle.Light;
                    }

                    var window = app.ConnectedScenes.OfType<UIKit.UIWindowScene>().SelectMany(s => s.Windows).FirstOrDefault(w => w.IsKeyWindow);

                    if (window != null)
                    {
                        window.OverrideUserInterfaceStyle = UIKit.UIUserInterfaceStyle.Light;
                    }

                    return false;
                });
            });
#endif
        });

        return @this;
    }

    private static void StopScrollBarsHiding()
    {
#if WINDOWS
        Microsoft.Maui.Handlers.ScrollViewHandler.Mapper.ModifyMapping(nameof(ScrollView.VerticalScrollBarVisibility), (h, v, r) =>
        {
            Microsoft.UI.Xaml.Setter settter = new Microsoft.UI.Xaml.Setter()
            {
                Property = Microsoft.UI.Xaml.Controls.Primitives.ScrollBar.IndicatorModeProperty,
                Value = Microsoft.UI.Xaml.Controls.Primitives.ScrollingIndicatorMode.MouseIndicator
            };

            var scroolBarType = typeof(Microsoft.UI.Xaml.Controls.Primitives.ScrollBar);

            Microsoft.UI.Xaml.Style style = new Microsoft.UI.Xaml.Style() { TargetType = scroolBarType };
            style.Setters.Add(settter);

            var scrollView = h.PlatformView;
            scrollView.Resources.Add(scroolBarType, style);
        });
#elif __ANDROID__
        Microsoft.Maui.Handlers.ScrollViewHandler.Mapper.AppendToMapping("FadeDuration", (h, v) =>
        {
            var platformView = h.PlatformView;
            if (platformView != null)
            {
                platformView.ScrollBarFadeDuration = 10;
                platformView.ScrollBarDefaultDelayBeforeFade = 10;
            }
        });

        Telerik.Maui.Handlers.RadScrollViewHandler.PlatformViewFactory =
            (handler) =>
            {
                LayoutInflater inflater = (LayoutInflater)handler.Context.GetSystemService(global::Android.App.Activity.LayoutInflaterService);
                var sv = (Com.Telerik.Widget.Primitives.Panels.RadScrollView)inflater.Inflate(Telerik.Maui.Core.Resource.Layout.scrollview_scrollbars, null, false);
                sv!.HorizontalScrollBarEnabled = false;
                sv.VerticalScrollBarEnabled = false;
                sv.ScrollbarFadingEnabled = true;

                return sv;
            };

        Microsoft.Maui.Handlers.LayoutHandler.PlatformViewFactory =
            (handler) =>
            {
                var virtualView = handler.VirtualView;
                if (virtualView is RadCollectionView collectionView)
                {
                    collectionView.VerticalScrollBarVisibility = ScrollBarVisibility.Never;
                    collectionView.HorizontalScrollBarVisibility = ScrollBarVisibility.Never;
                }

                return null;
            };

        Microsoft.Maui.Handlers.ContentViewHandler.PlatformViewFactory =
           (handler) =>
           {
               var virtualView = handler.VirtualView;
               if (virtualView is RadItemsView itemsView)
               {
                   itemsView.VerticalScrollBarVisibility = ScrollBarVisibility.Never;
                   itemsView.HorizontalScrollBarVisibility = ScrollBarVisibility.Never;
               }

               return null;
           };
#endif
    }

    private static void SetAutomationIds()
    {
#if __ANDROID__ || WINDOWS
        Microsoft.Maui.Handlers.ViewHandler.ViewMapper.AppendToMapping(nameof(IView.AutomationId), (h, v) => SetAutomationId(v));
        Microsoft.Maui.Handlers.ContentViewHandler.Mapper.AppendToMapping(nameof(IView.AutomationId), (h, v) => SetAutomationId(v));
        Microsoft.Maui.Handlers.ImageButtonHandler.Mapper.AppendToMapping(nameof(IView.AutomationId), (h, v) => SetAutomationId(v));
        Microsoft.Maui.Handlers.LabelHandler.Mapper.AppendToMapping(nameof(IView.AutomationId), (h, v) => SetAutomationId(v));
        Microsoft.Maui.Handlers.LayoutHandler.Mapper.AppendToMapping(nameof(IView.AutomationId), (h, v) => SetAutomationId(v));
        Microsoft.Maui.Handlers.PickerHandler.Mapper.AppendToMapping(nameof(IView.AutomationId), (h, v) => SetAutomationId(v));
        Microsoft.Maui.Handlers.RadioButtonHandler.Mapper.AppendToMapping(nameof(IView.AutomationId), (h, v) => SetAutomationId(v));
        Microsoft.Maui.Handlers.ScrollViewHandler.Mapper.AppendToMapping(nameof(IView.AutomationId), (h, v) => SetAutomationId(v));
        Microsoft.Maui.Handlers.SearchBarHandler.Mapper.AppendToMapping(nameof(IView.AutomationId), (h, v) => SetAutomationId(v));
        Microsoft.Maui.Handlers.SliderHandler.Mapper.AppendToMapping(nameof(IView.AutomationId), (h, v) => SetAutomationId(v));
        Microsoft.Maui.Handlers.SwitchHandler.Mapper.AppendToMapping(nameof(IView.AutomationId), (h, v) => SetAutomationId(v));
        Microsoft.Maui.Handlers.TimePickerHandler.Mapper.AppendToMapping(nameof(IView.AutomationId), (h, v) => SetAutomationId(v));
        Microsoft.Maui.Handlers.DatePickerHandler.Mapper.AppendToMapping(nameof(IView.AutomationId), (h, v) => SetAutomationId(v));
        Microsoft.Maui.Handlers.ButtonHandler.Mapper.AppendToMapping(nameof(IView.AutomationId), (h, v) => SetAutomationId(v));
        Maui.Handlers.RadButtonHandler.RadButtonMapper.AppendToMapping(nameof(IView.AutomationId), (h, v) => SetAutomationId(v));
        Maui.Handlers.RadBorderHandler.BorderMapper.AppendToMapping(nameof(IView.AutomationId), (h, v) => SetAutomationId(v));
        Maui.Handlers.RadItemsControlHandler.ItemsControlMapper.AppendToMapping(nameof(IView.AutomationId), (h, v) => SetAutomationId(v));
        Maui.Handlers.RadCheckBoxHandler.RadCheckBoxMapper.AppendToMapping(nameof(IView.AutomationId), (h, v) => SetAutomationId(v));
#if __ANDROID__
        // NOTE: The Semantics mapper sets MauiAccessibilityDelegateCompat that prevents ContentDescription from being set for EditText.
        // Because of that Semantics mapper is modified and does not do anything.
        // For reference see: https://github.com/dotnet/maui/blob/main/src/Core/src/Platform/Android/SemanticExtensions.cs#L26
        Microsoft.Maui.Handlers.EntryHandler.Mapper.ModifyMapping(nameof(IView.Semantics), (h, v, m) => { });
        Microsoft.Maui.Handlers.EntryHandler.Mapper.ModifyMapping(nameof(IView.AutomationId), (h, v, m) => SetEditTextContentDescription(h, v));
#endif
#endif
    }

#if __ANDROID__ || WINDOWS
    private static void SetAutomationId(IView v)
    {
        var automationId = v.AutomationId;
        if (!string.IsNullOrEmpty(automationId))
        {
            if (v is BindableObject element)
            {
#if WINDOWS
                // NOTE: If TextInput has placeholder don't set description. The default description of the platform control is the placeholder.
                if (element is Telerik.Maui.IRadTextInput textInput && !string.IsNullOrEmpty(textInput.Placeholder))
                {
                    return;
                }
#endif
                SemanticProperties.SetDescription(element, automationId);
            }
        }
    }
#endif

#if __ANDROID__
    private static void SetEditTextContentDescription(Microsoft.Maui.Handlers.IEntryHandler handler, IView view)
    {
        var automationId = view.AutomationId;
        if (!string.IsNullOrEmpty(automationId))
        {
            var platformView = handler.PlatformView;
            if (platformView != null)
            {
                platformView.ContentDescription = automationId;
            }
        }
    }
#endif

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

        port ??= DefaultTestCommandTcpPort;
        // Experimental TCP commands for testing
        try
        {
            Console.WriteLine("TEST COMMAND! Starting on port " + port.Value);
            var ipEndPoint = new IPEndPoint(IPAddress.Loopback, port.Value);
            tcpCommandServer = new TcpListener(ipEndPoint);
            tcpCommandServer.ExclusiveAddressUse = false;
            tcpCommandServer.Server.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReuseAddress, true);
            tcpCommandServer.Start();
            tcpCommandServer.BeginAcceptTcpClient(BeginAcceptTcpClientAsync, testingService);
            Console.WriteLine("TEST COMMAND! Listening on " + port.Value);
        }
        catch (Exception e)
        {
            Console.WriteLine("TEST COMMAND! Failed to start server.");
            Console.WriteLine(e);
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
            while ((line = await reader.ReadLineAsync()) != null)
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
                catch (Exception e)
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

    private static void StopEntryEmojiCompat()
    {
#if __ANDROID__
        Telerik.Maui.Handlers.RadTextInputHandler.PlatformViewFactory =
            (handler) =>
            {
                var editText = new Telerik.Maui.Platform.RadMauiTextInput(handler.Context);
                editText.EmojiCompatEnabled = false;

                return editText;
            };
#endif
    }
}