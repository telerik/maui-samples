using Microsoft.Maui.Controls;

namespace CryptoTracker.Views
{
    public partial class CoinHeaderView : ContentView
    {
        public static readonly BindableProperty CoinNameProperty =
            BindableProperty.Create(nameof(CoinName), typeof(string), typeof(CoinHeaderView));

        public static readonly BindableProperty CoinSymbolProperty =
            BindableProperty.Create(nameof(CoinSymbol), typeof(string), typeof(CoinHeaderView));

        public CoinHeaderView()
        {
            this.InitializeComponent();
        }

        public string CoinName
        {
            get => (string)GetValue(CoinNameProperty);
            set => SetValue(CoinNameProperty, value);
        }
        public string CoinSymbol
        {
            get => (string)GetValue(CoinSymbolProperty);
            set => SetValue(CoinSymbolProperty, value);
        }
    }
}