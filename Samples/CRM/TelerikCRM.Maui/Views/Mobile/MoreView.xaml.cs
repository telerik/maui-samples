using TelerikCRM.Maui.ViewModels;

namespace TelerikCRM.Maui.Views.Mobile;

public partial class MoreView
{
    public MoreView(MoreViewModel viewModel)
    {
        this.InitializeComponent();
        this.BindingContext = viewModel;
    }
}