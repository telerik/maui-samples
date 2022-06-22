using System;

namespace CryptoTracker.Data
{
    public class CoinData
    {
        public string Name { get; set; }
        public string Symbol { get; set; }
        public double OpeningPrice { get; set; }
        public double ClosingPrice { get; set; }
        public double Price24Low { get; set; }
        public double Price24High { get; set; }
        public DateTime Date { get; set; }
        public double ChangeInPriceAmount => this.ClosingPrice - this.OpeningPrice;
        public double ChangeInPricePercentage => (this.ChangeInPriceAmount / this.OpeningPrice) * 100;
    }
}
