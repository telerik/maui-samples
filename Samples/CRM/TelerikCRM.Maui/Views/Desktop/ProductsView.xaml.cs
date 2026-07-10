using Telerik.Maui.Controls;
using TelerikCRM.Maui.ViewModels;

namespace TelerikCRM.Maui.Views.Desktop;

public partial class ProductsView
{
    private RadPopup popup;

    public ProductsView()
    {
        this.InitializeComponent();

        var viewModel = IPlatformApplication.Current.Services.GetService<ProductsViewModel>();
        this.BindingContext = viewModel;

        viewModel.SearchCompleted += (s, e) => this.Dispatcher.DispatchDelayed(TimeSpan.FromMilliseconds(50), () => this.collectionView.ScrollItemIntoView(viewModel.SelectedProduct, false));
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
        ProductEditView editView = new ProductEditView();
        editView.SetBinding(View.BindingContextProperty, new Binding(nameof(ProductsViewModel.ProductViewModel), source: this.BindingContext));

        PopupWrapperView content = new PopupWrapperView() { Content = editView, MinimumHeightRequest = 350 };
        content.SetBinding(View.BindingContextProperty, new Binding(".", source: this.BindingContext));

        this.popup = new RadPopup()
        {
            IsModal = true,
            Placement = PlacementMode.Center,
            OutsideBackgroundColor = Colors.Black.WithAlpha(0.4f),
            Content = content
        };

        this.popup.SetBinding(RadPopup.IsOpenProperty, new Binding(nameof(ProductsViewModel.IsEditPopupOpen), source: this.BindingContext));
    }
}