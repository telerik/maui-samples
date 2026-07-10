using TelerikCRM.Maui.ViewModels;

namespace TelerikCRM.Maui.Views.Mobile;

public partial class AboutPage
{
    public AboutPage(AboutViewModel viewModel)
    {
        this.InitializeComponent();
        this.BindingContext = viewModel;
    }
}