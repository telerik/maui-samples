using Telerik.Maui.Controls;
using TelerikCRM.Maui.ViewModels;

namespace TelerikCRM.Maui.Views.Desktop;

public partial class OrdersView
{
    private RadPopup popup;

    public OrdersView(OrdersViewModel viewModel)
    {
        this.InitializeComponent();
        this.BindingContext = viewModel;
    }

    protected override void OnParentSet()
    {
        base.OnParentSet();

        if (this.popup == null)
        {
            this.Dispatcher.Dispatch(() => this.InitEditPopup());
        }
    }

    private void InitEditPopup()
    {
        OrderEditView editView = new OrderEditView();
        editView.SetBinding(View.BindingContextProperty, new Binding(nameof(OrdersViewModel.OrderViewModel), source: this.BindingContext));

        PopupWrapperView content = new PopupWrapperView() { Content = editView };
        content.SetBinding(View.BindingContextProperty, new Binding(".", source: this.BindingContext));

        this.popup = new RadPopup()
        {
            IsModal = true,
            Placement = PlacementMode.Center,
            OutsideBackgroundColor = Colors.Black.WithAlpha(0.4f),
            Content = content
        };
        this.popup.SetBinding(RadPopup.IsOpenProperty, new Binding(nameof(OrdersViewModel.IsEditPopupOpen), source: this.BindingContext));
    }

    private void OnDataGridLoaded(object sender, EventArgs e)
    {
#if WINDOWS
        foreach (var child in ((RadDataGrid)sender).Children)
        {
            if (child is RadScrollView)
            {
                if (child.Handler?.PlatformView is Telerik.Maui.Platform.RadMauiScrollView sv)
                {
                    sv.HorizontalScrollMode = Microsoft.UI.Xaml.Controls.ScrollMode.Disabled;
                    sv.VerticalScrollMode = Microsoft.UI.Xaml.Controls.ScrollMode.Disabled;
                }
            }
        }
#endif
    }
}