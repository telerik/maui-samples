using System.Collections.Generic;
using CryptoTracker.Data;
using Microsoft.Maui.Controls;

namespace CryptoTracker.ViewModels
{
    public class CoinsViewModel
    {
        public CoinsViewModel()
        {
            var coinService = DependencyService.Get<ICoinDataService>();
            this.Source = coinService.GetAllCurrentCoins();
            this.TrendingCoins = new List<TrendingCoinData>();

            var counter = 0; 
            foreach (var coin in this.Source)
            {
                if (coin.Name == "Bitcoin" || coin.Name == "Ethereum")
                {
                    this.TrendingCoins.Add(new TrendingCoinData()
                    {
                        Data = coin,
                        Index = ++counter,
                    });
                }
            }
        }

        public IList<CoinData> Source { get; }

        public IList<TrendingCoinData> TrendingCoins { get; }
    }
}
