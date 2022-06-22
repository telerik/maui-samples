using System;
using System.Collections.Generic;

namespace CryptoTracker.Data
{
    public interface ICoinDataService
    {
        IList<CoinData> GetCoinDataFromDateToDate(string coinName, DateTime fromDate, DateTime toDate);
        IList<CoinData> GetAllCurrentCoins();
        CoinData GetCurrentCoin(string coinPath);
    }
}
