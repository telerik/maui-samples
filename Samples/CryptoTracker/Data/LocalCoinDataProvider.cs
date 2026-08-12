using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CryptoTracker.Data
{
    public static class LocalCoinDataProvider
    {
        private static readonly IReadOnlyList<CoinData> coins = BuildCoins();

        public static Task<IList<CoinData>> GetCoinsAsync(int coinsCount)
        {
            var count = Math.Max(0, Math.Min(coinsCount, coins.Count));
            var result = coins
                .Take(count)
                .Select(CloneCoin)
                .ToList();

            return Task.FromResult<IList<CoinData>>(result);
        }

        public static Task<IList<CoinData>> GetOHLCCoinDataAsync(CoinData selectedCoin, int days)
        {
            var count = Math.Max(1, days);
            var data = BuildHistoricalData(selectedCoin, count, isHourly: false);
            return Task.FromResult<IList<CoinData>>(data);
        }

        public static Task<IList<CoinData>> GetHourlyOHLCCoinDataAsync(CoinData selectedCoin)
        {
            var data = BuildHistoricalData(selectedCoin, 24, isHourly: true);
            return Task.FromResult<IList<CoinData>>(data);
        }

        private static List<CoinData> BuildHistoricalData(CoinData coin, int points, bool isHourly)
        {
            var nowUtc = DateTime.UtcNow;
            var startPrice = coin?.OpeningPrice > 0 ? coin.OpeningPrice : 100;
            var seed = GetSeed((coin?.Symbol ?? "CRYPTO") + (isHourly ? "_H" : "_D"));
            var random = new Random(seed);
            var close = startPrice;
            var data = new List<CoinData>(points);

            for (int i = 0; i < points; i++)
            {
                var time = isHourly
                    ? nowUtc.AddHours(-(points - 1 - i))
                    : nowUtc.Date.AddDays(-(points - 1 - i));

                var volatility = isHourly ? 0.01 : 0.03;
                var open = Math.Max(0.01, close);
                var drift = (random.NextDouble() - 0.5) * volatility;
                close = Math.Max(0.01, open * (1 + drift));
                var high = Math.Max(open, close) * (1 + random.NextDouble() * volatility * 0.5);
                var low = Math.Max(0.01, Math.Min(open, close) * (1 - random.NextDouble() * volatility * 0.5));

                data.Add(new CoinData
                {
                    Name = coin?.Name,
                    Symbol = coin?.Symbol,
                    OpeningPrice = Math.Round(open, 2),
                    ClosingPrice = Math.Round(close, 2),
                    Price24High = Math.Round(high, 2),
                    Price24Low = Math.Round(low, 2),
                    UnixTimeStamp = new DateTimeOffset(time).ToUnixTimeSeconds(),
                });
            }

            return data;
        }

        private static IReadOnlyList<CoinData> BuildCoins()
        {
            var coinNames = new (string Symbol, string Name, double Price)[]
            {
                ("BTC", "Bitcoin", 68250.44),
                ("ETH", "Ethereum", 3380.12),
                ("USDT", "Tether", 1.00),
                ("BNB", "BNB", 604.22),
                ("SOL", "Solana", 162.15),
                ("XRP", "XRP", 0.61),
                ("USDC", "USD Coin", 1.00),
                ("DOGE", "Dogecoin", 0.18),
                ("ADA", "Cardano", 0.49),
                ("AVAX", "Avalanche", 37.42),
                ("SHIB", "Shiba Inu", 0.000028),
                ("DOT", "Polkadot", 7.43),
                ("TRX", "TRON", 0.13),
                ("MATIC", "Polygon", 0.74),
                ("LINK", "Chainlink", 16.52),
                ("TON", "Toncoin", 6.91),
                ("BCH", "Bitcoin Cash", 484.30),
                ("LTC", "Litecoin", 82.17),
                ("NEAR", "NEAR Protocol", 6.12),
                ("ICP", "Internet Computer", 12.45),
                ("APT", "Aptos", 8.97),
                ("UNI", "Uniswap", 10.84),
                ("PEPE", "Pepe", 0.000012),
                ("FIL", "Filecoin", 5.61),
                ("ATOM", "Cosmos", 8.36),
                ("XLM", "Stellar", 0.12),
                ("HBAR", "Hedera", 0.10),
                ("ARB", "Arbitrum", 1.24),
                ("OP", "Optimism", 2.31),
                ("ETC", "Ethereum Classic", 29.70),
                ("CRO", "Cronos", 0.12),
                ("VET", "VeChain", 0.04),
                ("ALGO", "Algorand", 0.19),
                ("MKR", "Maker", 2836.57),
                ("INJ", "Injective", 31.08),
                ("RNDR", "Render", 10.73),
                ("AAVE", "Aave", 104.36),
                ("IMX", "Immutable", 2.24),
                ("GRT", "The Graph", 0.30),
                ("SUI", "Sui", 1.16),
                ("SEI", "Sei", 0.64),
                ("RUNE", "THORChain", 5.74),
                ("FLOW", "Flow", 0.86),
                ("EGLD", "MultiversX", 41.03),
                ("KAS", "Kaspa", 0.17),
                ("XMR", "Monero", 167.42),
                ("QNT", "Quant", 117.25),
                ("SAND", "The Sandbox", 0.58),
                ("MANA", "Decentraland", 0.49),
                ("CHZ", "Chiliz", 0.13),
                ("AXS", "Axie Infinity", 8.21),
            };

            var result = new List<CoinData>(coinNames.Length);
            foreach (var item in coinNames)
            {
                var seed = GetSeed(item.Symbol);
                var random = new Random(seed);
                var opening = item.Price;
                var changePercent = Math.Round((random.NextDouble() - 0.5) * 12, 2);
                var changeAmount = Math.Round(opening * changePercent / 100, opening < 1 ? 6 : 2);
                var low = Math.Round(Math.Max(0.0000001, opening * (1 - Math.Abs(changePercent) / 100 * 0.8)), opening < 1 ? 6 : 2);
                var high = Math.Round(opening * (1 + Math.Abs(changePercent) / 100 * 0.8), opening < 1 ? 6 : 2);

                result.Add(new CoinData
                {
                    Name = item.Name,
                    Symbol = item.Symbol,
                    OpeningPrice = opening,
                    Price24Low = low,
                    Price24High = high,
                    ChangeInPriceAmount = changeAmount,
                    ChangeInPricePercentage = changePercent,
                    UnixTimeStamp = DateTimeOffset.UtcNow.ToUnixTimeSeconds(),
                });
            }

            return result;
        }

        private static CoinData CloneCoin(CoinData coin)
        {
            return new CoinData
            {
                Name = coin.Name,
                Symbol = coin.Symbol,
                OpeningPrice = coin.OpeningPrice,
                ClosingPrice = coin.ClosingPrice,
                Price24Low = coin.Price24Low,
                Price24High = coin.Price24High,
                UnixTimeStamp = coin.UnixTimeStamp,
                ChangeInPriceAmount = coin.ChangeInPriceAmount,
                ChangeInPricePercentage = coin.ChangeInPricePercentage,
            };
        }

        private static int GetSeed(string value)
        {
            unchecked
            {
                int hash = 17;
                for (int i = 0; i < value.Length; i++)
                {
                    hash = hash * 31 + value[i];
                }

                return Math.Abs(hash);
            }
        }
    }
}
