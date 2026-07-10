using TelerikCRM.Maui.ViewModels;

namespace TelerikCRM.Maui.Views.Mobile;

public partial class OrdersPage
{
    public OrdersPage(OrdersViewModel viewModel)
    {
        this.InitializeComponent();
        this.BindingContext = viewModel;
    }
}