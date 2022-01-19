using Microsoft.Maui.Controls;
using CryptoTracker.Views;
using CryptoTracker.Data;
using CryptoTracker.ViewModels;

namespace CryptoTracker.Pages
{
    public partial class DesktopPage : ContentPage
    {
        public DesktopPage()
        {
            this.InitializeComponent();
            this.InirializeSelection();
        }

        private void InirializeSelection()
        {
            var coinDataService = DependencyService.Get<ICoinDataService>();
            var initiallySelectedCoin = coinDataService.GetCurrentCoin("coin_Aave.csv");

            this.SelectCoin(initiallySelectedCoin);
        }

        private void OnCoinSelected(object sender, CoinSelectionEventArgs e)
        {
            this.SelectCoin(e.SelectedCoin);
        }

        private void SelectCoin(CoinData coinInfo)
        {
            var viewModel = (CoinInfoViewModel)this.selectedCoinInfo.BindingContext;
            viewModel.InitializeCoinData(coinInfo);
        }
    }
}
