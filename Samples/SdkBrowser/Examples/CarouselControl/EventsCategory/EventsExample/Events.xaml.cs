using Microsoft.Maui.Controls;
using Telerik.Maui.Controls.Carousel;

namespace SDKBrowserMaui.Examples.CarouselControl.EventsCategory.EventsExample;

public partial class Events : ContentView
{
    public Events()
    {
        InitializeComponent();
    }

    // >> carousel-events-handlers
    private void OnHighlightedItemChanged(object sender, CarouselHighlightedItemChangedEventArgs e)
    {
        var newTitle = (e.NewHighlightedItem as CarouselItem)?.Title ?? "none";
        this.highlightedLabel.Text = $"HighlightedItemChanged: {e.OldHighlightedIndex} -> {e.NewHighlightedIndex} ({newTitle})";
    }

    private void OnScrolled(object sender, CarouselScrolledEventArgs e)
    {
        this.scrolledLabel.Text = $"Scrolled: offset {e.ScrollOffset:F0}, item position {e.ItemPosition:F2}";
    }
    // << carousel-events-handlers
}
