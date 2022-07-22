using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace CryptoTracker.Data
{
    public class CoinDataService : ICoinDataService
    {
        private const string baseUrl = "https://min-api.cryptocompare.com/data";
        private const int CoinAPILimit = 100;
        private static async Task<string> GetAsync(string uri)
        {
            using var client = new HttpClient();
            var response = await client.GetStringAsync(uri);
            return response;
        }

        public async Task<IList<CoinData>> GetCoinsAsync(int coinsCount)
        {
            coinsCount = Math.Min(coinsCount, CoinAPILimit);
            var coins = new List<CoinData>();

            var uri = baseUrl + $"/top/totalvolfull?limit={coinsCount}&tsym=USD";
            var json = await GetAsync(uri);
            var coinsResponse = JsonDocument.Parse(json).RootElement.GetProperty("Data");
            for (int i = 0; i < coinsResponse.GetArrayLength(); i++)
            {
                var coinInfo = coinsResponse[i].GetProperty("CoinInfo");
                if (coinsResponse[i].TryGetProperty("RAW", out var rawData) && rawData.TryGetProperty("USD", out var usdData))
                {
                    var coinPrices = coinsResponse[i].GetProperty("RAW").GetProperty("USD");
                    var coin = new CoinData()
                    {
                        Symbol = coinInfo.GetProperty("Name").GetString(),
                        Name = coinInfo.GetProperty("FullName").GetString(),
                        Price24Low = coinPrices.GetProperty("LOWDAY").GetDouble(),
                        Price24High = coinPrices.GetProperty("HIGHDAY").GetDouble(),
                        OpeningPrice = coinPrices.GetProperty("OPENDAY").GetDouble(),
                        ChangeInPriceAmount = coinPrices.GetProperty("CHANGE24HOUR").GetDouble(),
                        ChangeInPricePercentage = coinPrices.GetProperty("CHANGEPCT24HOUR").GetDouble(),
                    };

                    coins.Add(coin);
                }
            }
            return coins;
        }

        public async Task<IList<CoinData>> GetOHLCCoinDataAsync(CoinData selectedCoin, int days)
        {
            var uri = $"{baseUrl}/v2/histoday?fsym={selectedCoin.Symbol}&tsym=USD&limit={days}";
            if (days == 1)
            {
                uri = $"{baseUrl}/v2/histohour?fsym={selectedCoin.Symbol}&tsym=USD&limit=24";
            }
            var doc = JsonDocument.Parse(await GetAsync(uri));
            var coinsJson = doc.RootElement.GetProperty("Data").GetProperty("Data");

            var coins = coinsJson.Deserialize<List<CoinData>>();

            foreach (var coin in coins)
            {
                coin.Name = selectedCoin.Name;
                coin.Symbol = selectedCoin.Symbol;
            }

            return coins;
        }

        public async Task<IList<CoinData>> GetHourlyOHLCCoinDataAsync(CoinData selectedCoin)
        {
            return await GetOHLCCoinDataAsync(selectedCoin, 1);
        }
    }
}
