using Telerik.Maui.Controls;
using TelerikCRM.Maui.ViewModels;

namespace TelerikCRM.Maui.Views.Desktop;

public partial class CustomersView
{
    private RadPopup createNewPopup;

    public CustomersView()
    {
        this.InitializeComponent();

        var viewModel = IPlatformApplication.Current.Services.GetService<CustomersViewModel>();
        this.BindingContext = viewModel;

        viewModel.SearchCompleted += (s, e) => this.Dispatcher.DispatchDelayed(TimeSpan.FromMilliseconds(50), () => this.collectionView.ScrollItemIntoView(viewModel.SelectedCustomer, false));
    }

    protected override void OnParentSet()
    {
        base.OnParentSet();

        if (this.createNewPopup == null)
        {
            this.Dispatcher.Dispatch(() => this.InitCreateNewPopup());
        }
    }

    private void InitCreateNewPopup()
    {
        CustomerEditView popupContent = new CustomerEditView();
        popupContent.SetBinding(View.BindingContextProperty, new Binding(nameof(CustomerDetailViewModel.EditViewModel), source: this.createNewButton.BindingContext));

        PopupWrapperView content = new PopupWrapperView() { Content = popupContent };
        content.SetBinding(View.BindingContextProperty, new Binding(".", source: this.createNewButton.BindingContext));

        this.createNewPopup = new RadPopup()
        {
            IsModal = true,
            Placement = PlacementMode.Center,
            OutsideBackgroundColor = Colors.Black.WithAlpha(0.4f),
            Content = content
        };

        this.createNewPopup.SetBinding(RadPopup.IsOpenProperty, new Binding(nameof(CustomerDetailViewModel.IsEditPopupOpen), source: this.createNewButton.BindingContext));
    }
}