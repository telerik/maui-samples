using Microsoft.Maui.Controls;
using System.Threading.Tasks;
using Telerik.Maui.Controls.CollectionView;

namespace SDKBrowserMaui.Examples.CollectionViewControl.ItemSwipeCategory.SwipeEventsExample;

public partial class SwipeEvents : ContentView
{
    public SwipeEvents()
    {
        InitializeComponent();
    }

    // >> collectionview-itemswipe-events-code
    private void OnSwipeStarting(object sender, CollectionViewSwipeStartingEventArgs e)
    {
        var item = (DataModel)e.Item;
        if(item.Country == "Spain")
        {
            e.Cancel = true;
            this.swipeActionLog.Text = $"Swiping cities in {item.Country} is canceled";
        }
    }

    private void OnSwiping(object sender, CollectionViewSwipingEventArgs e)
    {
        var item = (DataModel)e.Item;
        this.swipeActionLog.Text = $"Swiping {item.City} with Offset: {e.Offset:F0}";
    }

    private async void OnSwipeCompleted(object sender, CollectionViewSwipeCompletedEventArgs e)
    {
        var item = (DataModel)e.Item;
        this.swipeActionLog.Text = $"SwipeCompleted {item.City}";

        if (e.Offset <= -25)
        {
            await Task.Delay(200);
            (this.collectionView.BindingContext as ViewModel).Locations.Remove(item);

            this.swipeActionLog.Text += $", deleted {item.City}";
            this.collectionView.EndItemSwipe(false);
        }
        else if (e.Offset >= 25)
        {
            await Task.Delay(200);
            item.Visited = !item.Visited;

            this.swipeActionLog.Text += $", updated {item.City}";
            this.collectionView.EndItemSwipe();
        }
    }
    // << collectionview-itemswipe-events-code
}