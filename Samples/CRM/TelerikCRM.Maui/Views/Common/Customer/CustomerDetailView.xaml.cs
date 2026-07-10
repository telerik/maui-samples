#if MACCATALYST || WINDOWS
using Telerik.Maui.Controls;
using TelerikCRM.Maui.ViewModels;
#endif

namespace TelerikCRM.Maui.Views;

public partial class CustomerDetailView
{
#if MACCATALYST || WINDOWS
    private RadPopup editPopup;
    private RadPopup orderPopup;
#endif

    public CustomerDetailView()
    {
        this.InitializeComponent();
    }

#if MACCATALYST || WINDOWS
    protected override void OnParentSet()
    {
        base.OnParentSet();

        if (this.editPopup == null)
        {
            this.Dispatcher.Dispatch(this.InitEditPopup);
        }

        if (this.orderPopup == null)
        {
            this.Dispatcher.Dispatch(this.InitOrderPopup);
        }
    }

    private RadPopup GetPopupForContent(View popupContent)
    {
        Desktop.PopupWrapperView content = new Desktop.PopupWrapperView() { Content = popupContent };
        content.SetBinding(View.BindingContextProperty, new Binding(".", source: this.BindingContext));

        return new RadPopup()
        {
            IsModal = true,
            Placement = PlacementMode.Center,
            OutsideBackgroundColor = Colors.Black.WithAlpha(0.4f),
            Content = content
        };
    }

    private void InitEditPopup()
    {
        CustomerEditView editPopupContent = new CustomerEditView();
        editPopupContent.SetBinding(View.BindingContextProperty, new Binding(nameof(CustomerDetailViewModel.EditViewModel), source: this.BindingContext));
        this.editPopup = this.GetPopupForContent(editPopupContent);
        this.editPopup.SetBinding(RadPopup.IsOpenProperty, new Binding(nameof(CustomerDetailViewModel.IsEditPopupOpen), source: this.BindingContext));
    }

    private void InitOrderPopup()
    {
        OrderEditView popupContent = new OrderEditView();
        popupContent.SetBinding(View.BindingContextProperty, new Binding(nameof(CustomerDetailViewModel.OrderViewModel), source: this.BindingContext));
        this.orderPopup = this.GetPopupForContent(popupContent);
        this.orderPopup.SetBinding(RadPopup.IsOpenProperty, new Binding(nameof(CustomerDetailViewModel.IsOrderPopupOpen), source: this.BindingContext));
    }
#endif
}
