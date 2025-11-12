using Microsoft.Maui;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Devices;
using Microsoft.Maui.Graphics;
using Microsoft.Maui.Handlers;
using QSF.Helpers;
using QSF.Pages;
using QSF.Services;
using QSF.ViewModels;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Telerik.AppUtils.Services;
using Telerik.Maui.Controls;
using Application = Microsoft.Maui.Controls.Application;

namespace QSF;

public partial class App : Application
{
    private readonly ITestingService testingService;
    private readonly TelemetryService telemetryService;

    public App(ITestingService testingService, TelemetryService telemetryService)
    {
#if ANDROID
        // Setting the AccentColor from the Maui world here is needed
        // since the moment we try to access it from native Android is too early.
        Application.AccentColor = Color.FromArgb("#2B0B98");
#endif
        this.InitializeComponent();
        this.InitializeDependencies();

        this.testingService = testingService;
        this.telemetryService = telemetryService;

        this.testingService.OnCommand += (service, command) =>
        {
            if (command.Command.StartsWith("NAVIGATE:"))
            {
                var tcs = new TaskCompletionSource<string>();
                command.Result = tcs.Task;
                Dispatcher.Dispatch(async () =>
                {
                    try
                    {
                        var location = command.Command.Substring("NAVIGATE:".Length).Trim();
                        await DependencyService
                            .Get<INavigationService>()
                            .NavigateCommand(location);

                        tcs.SetResult("OK");
                    }
                    catch (Exception e)
                    {
                        tcs.SetException(e);
                    }
                });
            }
            else if (command.Command.StartsWith("GET EXAMPLES:"))
            {
                var examples = ((Application.Current.Windows[0].Page as NavigationPage).RootPage.BindingContext as HomeViewModel)?.Examples;
                command.Result =
                    Task.FromResult<string>("OK: " +
                        JsonSerializer.Serialize(
                            examples
                                .Select(e => new Dictionary<string, string>()
                                {
                                    { "Control", e.ControlName },
                                    { "Example", e.Name }
                                })
                                .ToList()));
            }
            else if (command.Command.StartsWith("THEME:"))
            {
                var themeString = command.Command.Substring("THEME:".Length).Trim();
                var theme = Enum.Parse<TelerikTheme>(themeString);
                ThemingViewModel.Instance.CurrentTheme = ThemingViewModel.Instance.ThemesList.First(t => t.Value == theme);
            }
        };

#if __ANDROID__ || __IOS__
        if (DeviceInfo.Idiom != DeviceIdiom.Desktop)
        {
            // TODO: When https://github.com/dotnet/maui/issues/5835 is really fixed, remove the following lines and the respective methods.
            // Currently, setting MaxLines of a Label to more than one for Android or iOS and LineBreakMode to TailTruncation
            // results to a single-line Label with truncation. The MaxLines is ignored.
            LabelHandler.Mapper.AppendToMapping(nameof(Label.LineBreakMode), UpdateMaxLines);
            LabelHandler.Mapper.AppendToMapping(nameof(Label.MaxLines), UpdateMaxLines);
        }
#endif
    }

    public static Color ApplicationAccentColor => (Color)App.Current.Resources["ApplicationAccentColor"];

    public static void DisplayAlert(string text)
    {
        var popup = new RadPopup();
        popup.IsModal = true;
        popup.Placement = PlacementMode.Center;
        popup.OutsideBackgroundColor = Color.FromArgb("#6F000000");

        var border = new RadBorder();
        border.CornerRadius = new Thickness(8);
        border.BackgroundColor = Color.FromArgb("#F1F1F1");

        var grid = new Grid();
        grid.Padding = new Thickness(10);
        grid.WidthRequest = 200;
        grid.HeightRequest = 150;
        grid.RowDefinitions.Add(new RowDefinition() { Height = GridLength.Star });
        grid.RowDefinitions.Add(new RowDefinition() { Height = 30 });

        var label = new Microsoft.Maui.Controls.Label();
        label.Text = text;
        label.VerticalOptions = LayoutOptions.Start;
        label.HorizontalOptions = LayoutOptions.Start;
        label.TextColor = Colors.Black;
        label.LineBreakMode = LineBreakMode.WordWrap;
        Grid.SetRow(label, 0);
        grid.Children.Add(label);

        var okButton = new RadButton();
        okButton.Padding = new Thickness(2);
        if (DeviceInfo.Platform != DevicePlatform.WinUI)
        {
            okButton.WidthRequest = 100;
        }
        okButton.HorizontalOptions = LayoutOptions.Center;
        okButton.VerticalOptions = LayoutOptions.End;
        okButton.BackgroundColor = Color.FromArgb("#674bb2");
        okButton.TextColor = Colors.White;
        okButton.Text = "OK";
        okButton.Clicked += (sender, args) =>
        {
            popup.IsOpen = false;
        };

        Grid.SetRow(okButton, 1);
        grid.Children.Add(okButton);
        border.Content = grid;
        popup.Content = border;
        popup.IsOpen = true;
    }

    protected override Window CreateWindow(IActivationState activationState)
    {
        Page mainPage;

        if (DeviceInfo.Idiom == DeviceIdiom.Desktop)
        {
            mainPage = new NavigationPage(new MainPageDesktop(this.testingService));
        }
        else
        {
            mainPage = new NavigationPage(new MainPageMobile(this.testingService))
            {
                BarTextColor = Colors.White
            };
        }

        var window = new Window(mainPage);
#if WINDOWS
        window.Title = "Telerik UI for .NET MAUI Controls Samples";
#endif

#if WINDOWS || MACCATALYST
        window.MinimumWidth = 1024;
        window.MinimumHeight = 768;
#endif

        var crashReporter = ServiceHelper.GetRequiredService<CrashReporter>();

        window.Destroying += async (_, __) =>
        {
            crashReporter?.Dispose();
            await this.FlushTelemetryAsync();
        };

        window.Stopped += async (_, __) =>
        {
            crashReporter?.Dispose();
            await this.FlushTelemetryAsync();
        };

        return window;
    }
    
    protected override void OnSleep()
    {
        base.OnSleep();
        // Fire-and-forget: flush pending telemetry and allow a brief delay for send
        _ = this.FlushTelemetryAsync();
    }

    protected override void OnStart()
    {
        base.OnStart();

        // Initialize version tracking so IsFirstLaunch* flags are accurate before sending telemetry
        Microsoft.Maui.ApplicationModel.VersionTracking.Track();

        // Track app start event
        this.telemetryService.TrackAppStart();
#if IOS
        UIKit.UINavigationController vc = (UIKit.UINavigationController)Microsoft.Maui.ApplicationModel.Platform.GetCurrentUIViewController();
        vc.InteractivePopGestureRecognizer.Delegate = new BackSwipeWithoutNavigationBar(vc);
        vc.InteractivePopGestureRecognizer.Enabled = true;

#endif
    }

#if __ANDROID__
    private static void UpdateMaxLines(ILabelHandler handler, ILabel label)
    {
        var textView = handler.PlatformView;
        if (label is Label controlsLabel && controlsLabel.MaxLines > -1 && textView.Ellipsize == Android.Text.TextUtils.TruncateAt.End)
        {
            textView.SetMaxLines(controlsLabel.MaxLines);
        }
    }
#elif __IOS__
    private static void UpdateMaxLines(ILabelHandler handler, ILabel label)
    {
        var labelView = handler.PlatformView;
        if (label is Label controlsLabel && labelView.LineBreakMode == UIKit.UILineBreakMode.TailTruncation)
        {
            labelView.Lines = controlsLabel.MaxLines;
        }
    }
#endif

    private void InitializeDependencies()
    {
        DependencyService.Register<IConfigurationService, ConfigurationService>();
        DependencyService.Register<IResourceService, AssemblyResourceService>();
        DependencyService.Register<IFileViewerService, FileViewerService>();
        DependencyService.Register<INavigationService, NavigationService>();
        DependencyService.Register<IExampleService, ExampleService>();
        DependencyService.Register<IConfigurationAreaService, ConfigurationAreaService>();
        DependencyService.Register<IControlsService, ControlsService>();
        DependencyService.Register<ISearchService, SearchService>();
        DependencyService.Register<IToastMessageService, ToastMessageService>();
        DependencyService.Register<ISerializationService, SerializationService>();
        DependencyService.Register<IFilePickerService, FilePickerService>();
        DependencyService.Register<IMediaPickerService, MediaPickerService>();
    }

    private async Task FlushTelemetryAsync()
    {
        try
        {
            this.telemetryService.Flush();
            // Give the channel a moment to send before the app is suspended/terminated
            await Task.Delay(500).ConfigureAwait(false);
        }
        catch
        {
            // Ignore flush errors; the app may be suspending/terminating and telemetry must not block or crash.
        }
    }

#if IOS
    public class BackSwipeWithoutNavigationBar : UIKit.UIGestureRecognizerDelegate
    {
        private UIKit.UINavigationController vc;

        public BackSwipeWithoutNavigationBar(UIKit.UINavigationController vc)
        {
            this.vc = vc;
        }

        public override bool ShouldBegin(UIKit.UIGestureRecognizer recognizer)
        {
            return this.vc.ViewControllers.Length > 1;
        }
    }
#endif
}
