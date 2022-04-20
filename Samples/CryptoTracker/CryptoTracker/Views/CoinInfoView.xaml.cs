using CryptoTracker.Data;
using CryptoTracker.ViewModels;
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
    }
}