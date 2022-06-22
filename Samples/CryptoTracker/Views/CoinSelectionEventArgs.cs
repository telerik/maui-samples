using CryptoTracker.Data;
using System;

namespace CryptoTracker.Views
{
    public class CoinSelectionEventArgs : EventArgs
    {
        public CoinSelectionEventArgs(CoinData selectedCoin)
        {
            this.SelectedCoin = selectedCoin;
        }

        public CoinData SelectedCoin { get; }
    }
}
