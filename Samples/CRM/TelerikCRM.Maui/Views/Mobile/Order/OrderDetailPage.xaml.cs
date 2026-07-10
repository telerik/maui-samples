using TelerikCRM.Maui.ViewModels;

namespace TelerikCRM.Maui.Views.Mobile;

public partial class OrderDetailPage
{
    public OrderDetailPage(OrderDetailViewModel viewModel)
    {
        this.InitializeComponent();
        this.BindingContext = viewModel;
    }
}