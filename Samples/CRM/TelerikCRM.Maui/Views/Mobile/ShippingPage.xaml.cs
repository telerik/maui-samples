using TelerikCRM.Maui.ViewModels;

namespace TelerikCRM.Maui.Views.Mobile;

public partial class ShippingPage
{
    public ShippingPage(ShippingViewModel viewModel)
    {
        this.InitializeComponent();
        this.BindingContext = viewModel;
        this.Loaded += this.OnLoaded;
    }

    private async void OnLoaded(object sender, EventArgs e)
    {
        await (BindingContext as ShippingViewModel)?.LoadShippingDataAsync()!;
        this.Loaded -= this.OnLoaded;
    }
}