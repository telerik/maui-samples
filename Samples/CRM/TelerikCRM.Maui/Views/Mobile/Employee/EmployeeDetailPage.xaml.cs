using TelerikCRM.Maui.ViewModels;

namespace TelerikCRM.Maui.Views.Mobile;

public partial class EmployeeDetailPage
{
    public EmployeeDetailPage(EmployeeDetailViewModel viewModel)
    {
        this.InitializeComponent();
        this.BindingContext = viewModel;
    }
}