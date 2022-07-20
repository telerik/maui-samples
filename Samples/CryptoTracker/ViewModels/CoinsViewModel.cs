using System.Collections.Generic;
using System.Linq;
using CryptoTracker.Data;
using Microsoft.Maui.Controls;
using Telerik.Maui.Controls;

namespace CryptoTracker.ViewModels
{
    public class CoinsViewModel : NotifyPropertyChangedBase
    {
        private const int trendingCoinsCount = 2;

        private IList<CoinData> source;
        private IList<TrendingCoinData> trendingCoins;
        private bool isListLoading;

        public CoinsViewModel()
        {
            this.Source = new List<CoinData>();
            this.TrendingCoins = new List<TrendingCoinData>();
            this.DownloadCoinsAsync(50);
        }

        public async void DownloadCoinsAsync(int coinsCount)
        {
            this.IsListLoading = true;
            var coinService = DependencyService.Get<ICoinDataService>();
            var coins = await coinService.GetCoinsAsync(coinsCount);
            coins = coins.Where(c => c.OpeningPrice >= 0.01).ToList();

            var topCoins = new List<TrendingCoinData>();

            for(int i = 0; i < trendingCoinsCount; i++)
            {
                topCoins.Add(new TrendingCoinData()
                {
                    Data = coins[i],
                    Index = i+1,
                });
            }

            this.Source = coins;
            this.TrendingCoins = topCoins;
            this.IsListLoading = false;
        }

        public IList<CoinData> Source
        {
            get => this.source;
            set => UpdateValue(ref this.source, value);
        }

        public IList<TrendingCoinData> TrendingCoins
        {
            get => this.trendingCoins;
            set => UpdateValue(ref this.trendingCoins, value);
        }

        public bool IsListLoading
        {
            get => this.isListLoading;
            set => UpdateValue(ref this.isListLoading, value);
        }
    }
}
