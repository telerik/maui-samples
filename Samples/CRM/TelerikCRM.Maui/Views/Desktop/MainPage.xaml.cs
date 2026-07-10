using Telerik.Maui.Controls;
using TelerikCRM.Maui.Common;
using TelerikCRM.Maui.Services;
using TelerikCRM.Maui.ViewModels;

namespace TelerikCRM.Maui.Views.Desktop;

public partial class MainPage : ContentPage
{
    public static new readonly BindableProperty IsBusyProperty =
        BindableProperty.Create(nameof(IsBusy), typeof(bool), typeof(ContentPage), false, BindingMode.TwoWay);

    private RadPopup welcomePopup;
    private WelcomeView welcomeView = new WelcomeView();
    private MainPageViewModel viewModel;

    public MainPage()
    {
        InitializeComponent();

        this.viewModel = IPlatformApplication.Current.Services.GetService<MainPageViewModel>();
        this.welcomeView.BoardingCompleted += this.BoardingCompleted;
    }

    public new bool IsBusy
    {
        get => (bool)this.GetValue(IsBusyProperty);
        set => this.SetValue(IsBusyProperty, value);
    }

    protected async override void OnNavigatedTo(NavigatedToEventArgs args)
    {
        base.OnNavigatedTo(args);
        await this.PrepareData();
    }

    protected override void OnBindingContextChanged()
    {
        base.OnBindingContextChanged();

        if (!Settings.IsFirstRun)
        {
            this.welcomePopup = null;
        }
        else
        {
            this.InitWelcomePopup();
            this.Dispatcher.DispatchDelayed(TimeSpan.FromMilliseconds(1000), () => this.welcomePopup.IsOpen = true);
        }
    }

    private void InitWelcomePopup()
    {
        this.welcomePopup = new RadPopup()
        {
            Content = this.welcomeView,
            Placement = PlacementMode.Center,
            PlacementTarget = this,
            OutsideBackgroundColor = Colors.Black.WithAlpha(0.4f),
            IsModal = true
        };
    }

    private async void BoardingCompleted(object sender, EventArgs e)
    {
        Settings.IsFirstRun = false;

        if (this.welcomePopup.IsOpen)
        {
            this.welcomePopup.IsOpen = false;
        }

        await this.PrepareData();
    }

    private async Task PrepareData()
    {
        await this.LoadDataAsync();
        this.BindingContext = this.viewModel;
    }

    private async Task LoadDataAsync()
    {
        IServiceProvider services = App.Current.Handler.MauiContext.Services;

        try
        {
            this.IsBusy = true;
            await services?.GetService<DatasyncClientService>().RefreshItemsAsync();
        }
        catch (Exception ex)
        {
            await services?.GetService<IAlertService>().DisplayAlertAsync("Error", $"There was a problem loading data. Details:\r\n\n{ex.Message}", "OK");
        }
        finally
        {
            this.IsBusy = false;
        }
    }
}
