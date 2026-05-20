using CryptoTracker.Data;
using Microsoft.Maui.Controls;
using System;

namespace CryptoTracker.Views;

public partial class CoinSelectionView : ContentView
{
    public CoinSelectionView()
    {
        this.InitializeComponent();
    }

    public event EventHandler<CoinSelectionEventArgs> CoinSelected;

    private void OnCollectionViewItemTapped(object sender, Telerik.Maui.RadTappedEventArgs<object> e)
    {
        this.trendingCollectionView.SelectedItem = null;
        this.CoinSelected?.Invoke(this, new CoinSelectionEventArgs((CoinData)e.Data));
    }

    private void OnTrendingCollectionViewItemTapped(object sender, Telerik.Maui.RadTappedEventArgs<object> e)
    {
        this.collectionView.SelectedItem = null;
        this.CoinSelected?.Invoke(this, new CoinSelectionEventArgs(((TrendingCoinData)e.Data).Data));
    }
}
