using Microsoft.Maui;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Controls.PlatformConfiguration.WindowsSpecific;
using Microsoft.Maui.Graphics;
using SDKBrowserMaui.Common;
using SDKBrowserMaui.Pages;
using SDKBrowserMaui.Services;
using System.Threading;
using Application = Microsoft.Maui.Controls.Application;
using Telerik.Maui.Controls.Compatibility.Primitives;
using Telerik.Maui.Controls.Compatibility.Input;
using Telerik.Maui.Controls;
using Microsoft.Maui.Devices;
using System;

namespace SDKBrowserMaui
{
    public partial class App : Application
    {
        public App()
        {
            this.UserAppTheme = Microsoft.Maui.ApplicationModel.AppTheme.Light;

            InitializeComponent();
            this.InitializeDependencies();

            if (Environment.GetEnvironmentVariable("EnableTelerikUIAutomation") == "true")
            {
                MainPage = new NavigationPage(new UITestsHomePage());
                UIAutomation.IsEnabled = true;
            }
            else
            {
                MainPage = new NavigationPage(new HomePage());
            }


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
            Telerik.Maui.Handlers.RadEntryHandler.EntryViewMapper.AppendToMapping(nameof(IView.AutomationId), (h, v) => SetAutomationId(v));
            Telerik.Maui.Handlers.RadButtonHandler.RadButtonMapper.AppendToMapping(nameof(IView.AutomationId), (h, v) => SetAutomationId(v));
            Telerik.Maui.Handlers.RadBorderHandler.BorderMapper.AppendToMapping(nameof(IView.AutomationId), (h, v) => SetAutomationId(v));
            Telerik.Maui.Handlers.RadItemsControlHandler.ItemsControlMapper.AppendToMapping(nameof(IView.AutomationId), (h, v) => SetAutomationId(v));
            Telerik.Maui.Handlers.RadCheckBoxHandler.RadCheckBoxMapper.AppendToMapping(nameof(IView.AutomationId), (h, v) => SetAutomationId(v));
#endif

        }

        private static void SetAutomationId(IView v)
        {
            var automationId = v.AutomationId;
            if (!string.IsNullOrEmpty(automationId))
            {
                BindableObject element = v as BindableObject;
                if (element != null)
                {
                    SemanticProperties.SetDescription(element, automationId);
                }
            }
        }

        private void InitializeDependencies()
        {
            DependencyService.Register<IConfigurationService, ConfigurationService>();
            DependencyService.Register<INavigationService, NavigationService>();
            DependencyService.Register<IExampleService, ExampleService>();
            DependencyService.Register<IBackdoorService, BackdoorService>();
        }

        // TODO: Remove this method once Application.Current.MainPage.DisplayAlert is fixed in Android
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

#if WINDOWS || MACCATALYST
        protected override Window CreateWindow(IActivationState activationState)
        {
            var window = base.CreateWindow(activationState);
            if (window != null)
            {
#if WINDOWS
                window.Title = "Telerik SDKBrowser Maui";
#endif

#if NET7_0_OR_GREATER
                window.MinimumWidth = 1024;
                window.MinimumHeight = 768;
#endif
            }

            return window;
        }
#endif
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
