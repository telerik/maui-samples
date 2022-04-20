using CryptoTracker.ViewModels;
using CryptoTracker.Data;
using Telerik.XamarinForms.Common;
using Microsoft.Maui.Controls;

namespace CryptoTracker.Views
{
    public partial class CoinInfoChartView : ContentView
    {
        public CoinInfoChartView()
        {
            this.InitializeComponent();
        }

        private void OnChartPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
#if __IOS__ || MACCATALYST
            // TODO: Remove this workaround after the following issue is fixed: https://github.com/dotnet/maui/issues/4849
            if (e.PropertyName == nameof(View.IsVisible))
            {
                var handler = ((Telerik.XamarinForms.Chart.RadCartesianChart)sender).Handler as global::UIKit.UIView;
                handler?.Superview.SetNeedsLayout();
            }
#endif
        }

        public void InitializeCoinData(CoinData coinInfo)
        {
            var viewModel = (CoinInfoViewModel)this.BindingContext;
            viewModel.InitializeCoinData(coinInfo);
        }

        public void ChangedTimePeriod(object sender, ValueChangedEventArgs<int> e)
        {
            if (this.chartTypesSegmentedControl == null)
            {
                return;
            }

            if (e.NewValue == 0)
            {
                this.chartTypesSegmentedControl.SetSegmentEnabled(1, false);
            }
            else
            {
                this.chartTypesSegmentedControl.SetSegmentEnabled(1, true);
            }
        }

        public void ChangedChartType(object sender, ValueChangedEventArgs<int> e)
        {
            if (this.timePeriodsSegmentedControl == null)
            {
                return;
            }

            if (e.NewValue == 1)
            {
                this.timePeriodsSegmentedControl.SetSegmentEnabled(0, false);
            }
            else
            {
                this.timePeriodsSegmentedControl.SetSegmentEnabled(0, true);
            }
        }
    }
}