using TelerikCRM.Maui.ViewModels;

namespace TelerikCRM.Maui.Views.Mobile;

public partial class OrderEditPage
{
    public OrderEditPage(OrderEditViewModel viewModel)
    {
        this.InitializeComponent();
        this.BindingContext = viewModel;
    }
}