using System;
using CryptoTracker.Data;
using Microsoft.Maui.Controls;
using Telerik.XamarinForms.DataControls.ListView;

namespace CryptoTracker.Views
{
    public partial class CoinSelectionView : ContentView
    {
        public CoinSelectionView()
        {
            this.InitializeComponent();
        }

        public event EventHandler<CoinSelectionEventArgs> CoinSelected;

        private void OnListViewItemTapped(object sender, ItemTapEventArgs e)
        {
            this.CoinSelected?.Invoke(this, new CoinSelectionEventArgs((CoinData)e.Item));
        }

        private void OnTrendingListViewItemTapped(object sender, ItemTapEventArgs e)
        {
            this.CoinSelected?.Invoke(this, new CoinSelectionEventArgs(((TrendingCoinData)e.Item).Data));
        }
    }
}
