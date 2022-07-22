using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CryptoTracker.Data
{
    public interface ICoinDataService
    {
        Task<IList<CoinData>> GetOHLCCoinDataAsync(CoinData selectedCoin, int days);
        Task<IList<CoinData>> GetCoinsAsync(int coinsCount);
        Task<IList<CoinData>> GetHourlyOHLCCoinDataAsync(CoinData selectedCoin);
    }
}
