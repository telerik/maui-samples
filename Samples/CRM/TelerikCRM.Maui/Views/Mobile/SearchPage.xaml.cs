using TelerikCRM.Maui.ViewModels;

namespace TelerikCRM.Maui.Views.Mobile;

public partial class SearchPage
{
    public SearchPage(SearchViewModel viewModel)
    {
        this.InitializeComponent();
        this.BindingContext = viewModel;
    }
}