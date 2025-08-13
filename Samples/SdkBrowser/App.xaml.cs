using Microsoft.Maui;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Graphics;
using SDKBrowserMaui.Common;
using SDKBrowserMaui.Pages;
using SDKBrowserMaui.Services;
using SDKBrowserMaui.ViewModels;
using System;
using System.Threading.Tasks;
using Telerik.AppUtils.Services;
using Telerik.Maui.Controls;
using Application = Microsoft.Maui.Controls.Application;

namespace SDKBrowserMaui
{
    public partial class App : Application
    {
        public App(ITestingService testingService)
        {
#if ANDROID
            // Setting the AccentColor from the Maui world here is needed
            // since the moment we try to access it from native Android is too early.
            Application.AccentColor = Color.FromArgb("#2B0B98");
#endif

            this.UserAppTheme = Microsoft.Maui.ApplicationModel.AppTheme.Light;

            this.InitializeComponent();
            this.InitializeDependencies();

            testingService.OnCommand += (service, command) =>
            {
                HandleTestingServiceCommand(service, command);
            };
        }

        private void InitializeDependencies()
        {
            DependencyService.Register<IConfigurationService, ConfigurationService>();
            DependencyService.Register<INavigationService, NavigationService>();
            DependencyService.Register<IExampleService, ExampleService>();
            DependencyService.Register<IBackdoorService, BackdoorService>();
            DependencyService.RegisterSingleton<ThemeSettingsViewModel>(new ThemeSettingsViewModel());
        }

        // TODO: Remove this method once Application.Current.Windows[0].Page.DisplayAlert is fixed in Android
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
#if WINDOWS
            okButton.HandlerChanged += (sender, args) =>
            {
                var platformView = okButton.Handler?.PlatformView as global::Microsoft.UI.Xaml.FrameworkElement;
                if (platformView != null)
                {
                    platformView.IsTabStop = false;
                    platformView.AllowFocusOnInteraction = false;
                }
            };
#endif
            okButton.Padding = new Thickness(2);
            okButton.WidthRequest = 100;
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
            var window = new Window(new NavigationPage(new HomePage()));
#if WINDOWS || MACCATALYST
            window.Title = "Telerik SDKBrowser Maui";
            window.MinimumWidth = 1024;
            window.MinimumHeight = 768;
#endif
            return window;
        }

        private static bool TryParseNavigateToExample(string location, out string control, out string example)
        {
            control = example = null;
            var parts = location.Split('.', 2);
            if (parts.Length == 2)
            {
                control = parts[0];
                example = parts[1];
                return true;
            }

            return false;
        }

        private void HandleTestingServiceCommand(object service, TestCommandEventArgs e)
        {
            var command = e.Command;
            if (command.StartsWith("NAVIGATE:", StringComparison.OrdinalIgnoreCase))
            {
                this.HandleNavigateCommand(e);
                return;
            }

            if (command.StartsWith("THEME:", StringComparison.OrdinalIgnoreCase))
            {
                this.HandleThemeCommand(e);
                return;
            }

            e.Result = Task.FromResult("Unknown command");
        }

        private void HandleNavigateCommand(TestCommandEventArgs e)
        {
            var command = e.Command;
            var backdoorService = DependencyService.Get<IBackdoorService>();
            var tcs = new TaskCompletionSource<string>();
            e.Result = tcs.Task;

            Dispatcher.Dispatch(async () =>
            {
                try
                {
                    string location = command["NAVIGATE:".Length..].Trim();

                    if (TryParseNavigateToExample(location, out var control, out var example))
                    {
                        await backdoorService.NavigateToExampleAsync(control, example);
                        tcs.SetResult("OK");
                    }
                    else if (location.Equals("SEARCH", StringComparison.OrdinalIgnoreCase))
                    {
                        await backdoorService.NavigateToSearchAsync();
                        tcs.SetResult("OK");
                    }
                    else if (location.Equals("BACK", StringComparison.OrdinalIgnoreCase))
                    {
                        await backdoorService.NavigateBackAsync();
                        tcs.SetResult("OK");
                    }
                    else
                    {
                        tcs.SetResult($"{command} not recognized");
                    }
                }
                catch (Exception ex)
                {
                    tcs.SetException(ex);
                }
            });
        }

        private void HandleThemeCommand(TestCommandEventArgs e)
        {
            var command = e.Command;
            var backdoorService = DependencyService.Get<IBackdoorService>();
            var tcs = new TaskCompletionSource<string>();
            e.Result = tcs.Task;

            Dispatcher.Dispatch(() =>
            {
                try
                {
                    string themeArgs = command["THEME:".Length..].Trim();
                    var parts = themeArgs.Split('.', 2);

                    if (parts.Length == 2)
                    {
                        bool themeSet = backdoorService.TrySetTheme(parts[0], parts[1]);
                        tcs.SetResult(themeSet ? "OK" : $"{parts[0]}.{parts[1]} not recognized");
                    }
                    else
                    {
                        tcs.SetResult($"{command} not recognized");
                    }
                }
                catch (Exception ex)
                {
                    tcs.SetException(ex);
                }
            });
        }
    }

#if __ANDROID__
    // TODO: This is a temp workaround that prevents the linker from removing the constructor of the TextCellRenderer used by the ListView.
    // As soon as Maui provides more linker options it can be removed.
    [global::Android.Runtime.Preserve]
    public class DummyTextCellRenderer : Microsoft.Maui.Controls.Handlers.Compatibility.TextCellRenderer
    {

    }
#endif
}
