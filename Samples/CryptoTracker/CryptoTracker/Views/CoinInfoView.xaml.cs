using CryptoTracker.ViewModels;
using CryptoTracker.Data;
using Telerik.XamarinForms.Common;
using Microsoft.Maui.Controls;

namespace CryptoTracker.Views
{
    public partial class CoinInfoView : ContentView
    {
        public CoinInfoView()
        {
            this.InitializeComponent();
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