using Telerik.Maui.Controls;
using TelerikCRM.Maui.ViewModels;

namespace TelerikCRM.Maui.Views.Desktop;

public partial class EmployeesView
{
    private RadPopup createNewPopup;

    public EmployeesView()
    {
        this.InitializeComponent();

        var viewModel = IPlatformApplication.Current.Services.GetService<EmployeesViewModel>();
        this.BindingContext = viewModel;

        viewModel.SearchCompleted += (s, e) => this.Dispatcher.DispatchDelayed(TimeSpan.FromMilliseconds(50), () => this.collectionView.ScrollItemIntoView(viewModel.SelectedEmployee, false));
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
        EmployeeEditView popupContent = new EmployeeEditView();
        popupContent.SetBinding(View.BindingContextProperty, new Binding(nameof(EmployeeDetailViewModel.EditViewModel), source: this.createNewButton.BindingContext));

        PopupWrapperView content = new PopupWrapperView() { Content = popupContent };
        content.SetBinding(View.BindingContextProperty, new Binding(".", source: this.createNewButton.BindingContext));

        this.createNewPopup = new RadPopup()
        {
            IsModal = true,
            Placement = PlacementMode.Center,
            OutsideBackgroundColor = Colors.Black.WithAlpha(0.4f),
            Content = content
        };

        this.createNewPopup.SetBinding(RadPopup.IsOpenProperty, new Binding(nameof(EmployeeDetailViewModel.IsEditPopupOpen), source: this.createNewButton.BindingContext));
    }
}