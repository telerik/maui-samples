using TelerikCRM.Maui.ViewModels;

namespace TelerikCRM.Maui.Views.Mobile;

public partial class EmployeesView
{
    public EmployeesView(EmployeesViewModel viewModel)
    {
        this.InitializeComponent();
        this.BindingContext = viewModel;
    }
}