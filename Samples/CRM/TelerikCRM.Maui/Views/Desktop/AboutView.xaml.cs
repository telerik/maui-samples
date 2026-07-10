using TelerikCRM.Maui.ViewModels;

namespace TelerikCRM.Maui.Views.Desktop;

public partial class AboutView
{
    public AboutView(AboutViewModel viewModel)
    {
        this.InitializeComponent();
        this.BindingContext = viewModel;
    }
}