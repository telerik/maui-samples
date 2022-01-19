using CryptoTracker.Data;
using CryptoTracker.ViewModels;
using Microsoft.Maui.Controls;

namespace CryptoTracker.Pages
{
    public partial class CoinInfoPage : ContentPage
    {
        public CoinInfoPage()
        {
            this.InitializeComponent();
        }

        public void InitializeCoinData(CoinData coinInfo)
        {
            var viewModel = (CoinInfoViewModel)this.coinInfoView.BindingContext;
            viewModel.InitializeCoinData(coinInfo);
        }
    }
}