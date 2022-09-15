using Microsoft.Maui.Controls;
using CryptoTracker.Views;
using CryptoTracker.Data;
using CryptoTracker.ViewModels;
using System.Linq;

namespace CryptoTracker.Pages
{
    public partial class DesktopPage : ContentPage
    {
        public DesktopPage()
        {
            this.InitializeComponent();
            this.InitializeSelection();
        }

        private async void InitializeSelection()
        {
            this.BusyIndicator.IsBusy = true;
            var coinDataService = DependencyService.Get<ICoinDataService>();
            var initiallySelectedCoin = (await coinDataService.GetCoinsAsync(1)).FirstOrDefault();
            if (initiallySelectedCoin != null)
            {
                this.SelectCoin(initiallySelectedCoin);
            }
            this.BusyIndicator.IsBusy = false;
        }

        private void OnCoinSelected(object sender, CoinSelectionEventArgs e)
        {
            this.SelectCoin(e.SelectedCoin);
        }

        private void SelectCoin(CoinData coinInfo)
        {
            var viewModel = (CoinInfoViewModel)this.BindingContext;
            viewModel.InitializeCoinData(coinInfo);
        }

        private void DownloadButtonClicked(object sender, System.EventArgs e)
        {
            Utils.Utils.TryNavigateToDownloadUrl();
        }
    }
}
