using TelerikCRM.Maui.Common;
using TelerikCRM.Maui.Services;
using TelerikCRM.Maui.ViewModels;

namespace TelerikCRM.Maui.Views.Mobile;

public partial class MainPage : ContentPage
{
    private readonly WelcomeView welcomeView = new WelcomeView();
    private readonly MainPageViewModel viewModel;

    public MainPage()
    {
        InitializeComponent();

        this.viewModel = new MainPageViewModel();
        this.viewModel.IsBusy = true;

        this.mainBusyIndicator.BindingContext = this.viewModel;

        this.welcomeView.BoardingCompleted += this.BoardingCompleted;

        if (!Settings.IsFirstRun)
        {
            this.RemoveWelcomeViewFromUI();
        }
        else
        {
            this.AddWelcomeViewToUI();
        }
    }

    protected async override void OnNavigatedTo(NavigatedToEventArgs args)
    {
        base.OnNavigatedTo(args);

        if (!Settings.IsFirstRun)
        {
            await this.PrepareData();
        }
    }

    private async void BoardingCompleted(object sender, EventArgs e)
    {
        this.RemoveWelcomeViewFromUI();
        Settings.IsFirstRun = false;

        await this.PrepareData();
    }

    private async Task PrepareData()
    {
        // NOTE: This condition is applicable only for when app is in read-only mode.
        if (this.BindingContext == null)
        {
            await this.LoadDataAsync();
            this.BindingContext = this.viewModel;
        }
    }

    private async Task LoadDataAsync()
    {
        IServiceProvider services = App.Current.Handler.MauiContext.Services;

        try
        {
            this.viewModel.IsBusyMessage = "Telerik CRM Initializing";
            this.viewModel.IsBusy = true;
            await services?.GetService<DatasyncClientService>().RefreshItemsAsync();
        }
        catch (Exception ex)
        {
            await services?.GetService<IAlertService>().DisplayAlertAsync("Error", $"There was a problem loading data. Details:\r\n\n{ex.Message}", "OK");
        }
        finally
        {
            this.viewModel.IsBusyMessage = "";
            this.viewModel.IsBusy = false;
        }
    }

    private void AddWelcomeViewToUI()
    {
        if (this.Content is Layout layout)
        {
            Grid.SetRowSpan(this.welcomeView, 2);
            layout.Add(this.welcomeView);
        }
    }

    private void RemoveWelcomeViewFromUI()
    {
        if (this.Content is Layout layout)
        {
            layout.Remove(this.welcomeView);
        }
    }

    private void OnTabViewSelecionChnaged(object sender, EventArgs e)
    {
        object context = null;

        var selectedTabItem = this.tabView.SelectedItem;
        if (selectedTabItem.Content is ContentView tabItemContent)
        {
            context = tabItemContent.Content?.BindingContext;
        }

        this.header.BindingContext = context;
    }
}
