using Microsoft.Maui.Controls;
using CryptoTracker.Views;

namespace CryptoTracker.Pages
{
    public partial class CoinSelectionPage : ContentPage
    {
        public CoinSelectionPage()
        {
            this.InitializeComponent();
        }

        private void OnCoinSelected(object sender, CoinSelectionEventArgs e)
        {
            var coinInfo = e.SelectedCoin;
            var destination = new CoinInfoPage()
            {
                Title = $"{coinInfo.Name} {coinInfo.Symbol}",
            };

            destination.InitializeCoinData(coinInfo);
            this.Navigation.PushAsync(destination, true);
        }

        private void DownloadButtonClicked(object sender, System.EventArgs e)
        {
            Utils.Utils.TryNavigateToDownloadUrl();
        }
    }
}
