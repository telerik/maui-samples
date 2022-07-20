using System;
using System.Text.Json.Serialization;

namespace CryptoTracker.Data
{
    public class CoinData
    {
        public string Name { get; set; }
        public string Symbol { get; set; }

        [JsonPropertyName("open")]
        public double OpeningPrice { get; set; }

        [JsonPropertyName("close")]
        public double ClosingPrice { get; set; }

        [JsonPropertyName("low")]
        public double Price24Low { get; set; }

        [JsonPropertyName("high")]
        public double Price24High { get; set; }

        [JsonPropertyName("time")]
        public double UnixTimeStamp { get; set; }
        public DateTime Date => new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc).AddSeconds(this.UnixTimeStamp).ToLocalTime();
        public double ChangeInPriceAmount { get; set; }
        public double ChangeInPricePercentage { get; set; }
    }
}
