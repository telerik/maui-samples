using TelerikCRM.Maui.ViewModels;

namespace TelerikCRM.Maui.Views.Mobile;

public partial class EmployeeEditPage
{
    public EmployeeEditPage(EmployeeEditViewModel viewModel)
    {
        this.InitializeComponent();
        this.BindingContext = viewModel;
    }
}