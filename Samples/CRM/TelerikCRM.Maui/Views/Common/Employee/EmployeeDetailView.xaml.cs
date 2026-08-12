using Telerik.Maui.Controls;
using Telerik.Maui.Controls.Compatibility.Chart;
using TelerikCRM.Maui.ViewModels;

namespace TelerikCRM.Maui.Views;

public partial class EmployeeDetailView
{
#if MACCATALYST || WINDOWS
    private RadPopup editPopup;
    private RadPopup orderPopup;
#endif

    private EmployeeDetailViewModel viewModel;

    public EmployeeDetailView()
    {
        this.InitializeComponent();
        this.CompensationChart.HandlerChanged += this.OnPieChartHandlerChanged;
        this.SalesHistoryChart.HandlerChanged += this.OnSalesHistoryChartHandlerChanged;
    }

    private void OnPieChartHandlerChanged(object sender, EventArgs e)
    {
#if WINDOWS
        if (this.CompensationChart.Handler?.PlatformView is Telerik.UI.Xaml.Controls.Chart.RadPieChart chart)
        {
            chart.Series.First().HighlightBrush = new Microsoft.UI.Xaml.Media.SolidColorBrush(Windows.UI.Color.FromArgb(0, 0, 0, 0));
        }
#endif
    }

    protected override async void OnBindingContextChanged()
    {
        base.OnBindingContextChanged();

        this.viewModel = this.BindingContext as EmployeeDetailViewModel;
        if (this.viewModel != null)
        {
#if MACCATALYST || WINDOWS
            this.viewModel.PropertyChanged -= this.OnViewModelPropertyChanged;
            this.viewModel.PropertyChanged += this.OnViewModelPropertyChanged;
#else
            await this.viewModel.PrepareGaugeDataAsync();
#endif
        }
    }

    private void OnSalesHistoryChartHandlerChanged(object sender, EventArgs e)
    {
        this.UpdateChart();
    }

    private void UpdateChart()
    {
        var platformView = this.SalesHistoryChart.Handler?.PlatformView;
#if IOS || MACCATALYST
        if (platformView is not Telerik.Maui.Controls.Compatibility.ChartRenderer.iOS.TKExtendedChart platformChart)
        {
            return;
        }

        platformChart.YAxis.Style.LabelStyle.TextAlignment = TelerikUI.TKChartAxisLabelAlignment.Left;
        platformChart.YAxis.Style.LabelStyle.FirstLabelTextAlignment = TelerikUI.TKChartAxisLabelAlignment.Left;
#endif
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
        EmployeeEditView editPopupContent = new EmployeeEditView();
        editPopupContent.SetBinding(View.BindingContextProperty, new Binding(nameof(EmployeeDetailViewModel.EditViewModel), source: this.BindingContext));
        this.editPopup = this.GetPopupForContent(editPopupContent);
        this.editPopup.SetBinding(RadPopup.IsOpenProperty, new Binding(nameof(EmployeeDetailViewModel.IsEditPopupOpen), source: this.BindingContext));
    }

    private void InitOrderPopup()
    {
        OrderEditView popupContent = new OrderEditView();
        popupContent.SetBinding(View.BindingContextProperty, new Binding(nameof(EmployeeDetailViewModel.OrderViewModel), source: this.BindingContext));
        this.orderPopup = this.GetPopupForContent(popupContent);
        this.orderPopup.SetBinding(RadPopup.IsOpenProperty, new Binding(nameof(EmployeeDetailViewModel.IsOrderPopupOpen), source: this.BindingContext));
    }

    private async void OnViewModelPropertyChanged(object s, System.ComponentModel.PropertyChangedEventArgs e)
    {
        if (e.PropertyName == nameof(EmployeeDetailViewModel.SelectedEmployee))
        {
            await this.viewModel.PrepareGaugeDataAsync();
        }
        this.UpdateChart();
    }
#endif
}

public class DateLabelFormatter : LabelFormatterBase<DateTime>
{
    public override string FormatTypedValue(DateTime value)
    {
        return value.ToString("MMM yy");
    }
}
