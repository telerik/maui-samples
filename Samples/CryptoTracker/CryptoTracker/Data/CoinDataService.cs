using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace CryptoTracker.Data
{
    public class CoinDataService : ICoinDataService
    {
        private readonly string[] coinFilePaths;

        public CoinDataService()
        {
            var assembly = typeof(CoinDataService).Assembly;

            this.coinFilePaths = assembly.GetManifestResourceNames().Where(x => x.StartsWith("CryptoTracker.Data.Coins")).ToArray();
        }

        public IList<CoinData> GetAllCurrentCoins()
        {
            var coins = new List<CoinData>();
            foreach (var path in this.coinFilePaths)
            {
                coins.Add(GetCurrentCoin(path));
            }

            foreach (var coin in coins)
            {
                Console.WriteLine(coin.ToString());
            }

            return coins;
        }

        public IList<CoinData> GetCoinDataFromDateToDate(string coinName, DateTime start, DateTime end)
        {
            var coinInfo = new List<CoinData>();
            var coinFile = this.coinFilePaths.Where(x => x.Contains(coinName.Replace(" ", "").Replace(".", ""), StringComparison.OrdinalIgnoreCase)).First();
            var assembly = typeof(CoinDataService).Assembly;

            using (var input = assembly.GetManifestResourceStream(coinFile))
            {
                using (var reader = new StreamReader(input))
                {
                    if (reader != null)
                    {
                        reader.ReadLine();
                    }

                    while (!reader.EndOfStream)
                    {
                        var line = reader.ReadLine().Split(',');
                        var currentCoin = new CoinData()
                        {
                            Name = line[1],
                            Symbol = line[2],
                            Date = DateTime.Parse(line[3]),
                            Price24High = double.Parse(line[4]),
                            Price24Low = double.Parse(line[5]),
                            OpeningPrice = double.Parse(line[6]),
                            ClosingPrice = double.Parse(line[7]),
                        };

                        if (currentCoin.Date >= start && currentCoin.Date < end)
                        {
                            coinInfo.Add(currentCoin);
                        }
                    }
                }
            }

            return coinInfo;
        }

        public CoinData GetCurrentCoin(string coinPath)
        {
            var coinPathToGet = this.coinFilePaths.Where(x => x.Contains($"{coinPath}")).First();
            var assembly = typeof(CoinDataService).Assembly;

            using (var input = assembly.GetManifestResourceStream(coinPathToGet))
            {
                using (var reader = new StreamReader(input))
                {
                    var lastLine = string.Empty;

                    while (!reader.EndOfStream)
                    {
                        lastLine = reader.ReadLine();
                    }

                    var splitedLastLine = lastLine.Split(',');
                    var currentCoin = new CoinData()
                    {
                        Name = splitedLastLine[1],
                        Symbol = splitedLastLine[2],
                        Date = DateTime.Parse(splitedLastLine[3]),
                        Price24High = double.Parse(splitedLastLine[4]),
                        Price24Low = double.Parse(splitedLastLine[5]),
                        OpeningPrice = double.Parse(splitedLastLine[6]),
                        ClosingPrice = double.Parse(splitedLastLine[7]),
                    };

                    return currentCoin;
                }
            }
        }
    }
}
