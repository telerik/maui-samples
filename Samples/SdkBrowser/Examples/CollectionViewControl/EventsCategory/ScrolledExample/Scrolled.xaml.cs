using Microsoft.Maui.Controls;

namespace SDKBrowserMaui.Examples.CollectionViewControl.EventsCategory.ScrolledExample;

public partial class Scrolled : ContentView
{
    public Scrolled()
    {
        InitializeComponent();
    }

    // >> collectionview-scrolled-event
    private void OnScrolled(object sender, ScrolledEventArgs e)
    {
        this.scrollInfoLabel.Text = $"Scroll position x: {System.Math.Round(e.ScrollX, 2)}, y: {System.Math.Round(e.ScrollY, 2)}";
    }
    // << collectionview-scrolled-event
}