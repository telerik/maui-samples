using Microsoft.Maui;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Devices;
using Microsoft.Maui.Graphics;
using Microsoft.Maui.Handlers;
using QSF.Pages;
using QSF.Services;
using Telerik.Maui.Controls;
using Application = Microsoft.Maui.Controls.Application;

namespace QSF;

public partial class App : Application
{
    public App()
    {
        this.UserAppTheme = Microsoft.Maui.ApplicationModel.AppTheme.Light;

        this.InitializeComponent();
        this.InitializeDependencies();

        if (DeviceInfo.Idiom == DeviceIdiom.Desktop)
        {
            this.MainPage = new NavigationPage(new MainPageDesktop());
        }
        else
        {
            this.MainPage = new NavigationPage(new MainPageMobile());

#if __ANDROID__ || __IOS__
            // TODO: When https://github.com/dotnet/maui/issues/5835 is really fixed, remove the following lines and the respective methods.
            // Currently, setting MaxLines of a Label to more than one for Android or iOS and LineBreakMode to TailTruncation
            // results to a single-line Label with truncation. The MaxLines is ignored.
            LabelHandler.Mapper.AppendToMapping(nameof(Label.LineBreakMode), UpdateMaxLines);
            LabelHandler.Mapper.AppendToMapping(nameof(Label.MaxLines), UpdateMaxLines);
#endif
        }
    }

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

#if __ANDROID__
    private static void UpdateMaxLines(ILabelHandler handler, ILabel label)
    {
        var textView = handler.PlatformView;
        if (label is Label controlsLabel && textView.Ellipsize == Android.Text.TextUtils.TruncateAt.End)
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

#if WINDOWS || MACCATALYST
    protected override Window CreateWindow(IActivationState activationState)
    {
        var window = base.CreateWindow(activationState);
        if (window != null)
        {
#if WINDOWS
            window.Title = "Telerik UI for .NET MAUI Controls Samples";
#endif

#if NET7_0_OR_GREATER
            window.MinimumWidth = 1024;
            window.MinimumHeight = 768;
#endif
        }

        return window;
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
}
