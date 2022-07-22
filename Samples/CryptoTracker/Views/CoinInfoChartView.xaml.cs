using CryptoTracker.ViewModels;
using CryptoTracker.Data;
using Telerik.Maui.Controls.Compatibility.Common;
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
                var handler = ((Telerik.Maui.Controls.Compatibility.Chart.RadCartesianChart)sender).Handler as global::UIKit.UIView;
                handler?.Superview.SetNeedsLayout();
            }
#endif
        }

        public void InitializeCoinData(CoinData coinInfo)
        {
            var viewModel = (CoinInfoViewModel)this.BindingContext;
            viewModel.InitializeCoinData(coinInfo);
        }
    }
}