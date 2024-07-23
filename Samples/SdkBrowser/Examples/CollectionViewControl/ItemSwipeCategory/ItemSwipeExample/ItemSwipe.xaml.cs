using Microsoft.Maui.Controls;
using System.Collections.ObjectModel;

namespace SDKBrowserMaui.Examples.CollectionViewControl.ItemSwipeCategory.ItemSwipeExample;

public partial class ItemSwipe : ContentView
{
	public ItemSwipe()
	{
		InitializeComponent();
	}

    // >> collectionview-itemswipe-code
    private void DeleteItemTapped(object sender, TappedEventArgs e)
    {
        var item = ((View)sender).BindingContext as DataModel;
        if (item != null)
        {
            this.collectionView.EndItemSwipe(true, animationFinished: () =>
            {
                (this.collectionView.BindingContext as ViewModel).Locations.Remove(item);
            });
        }
    }

    private void VisitItemTapped(object sender, TappedEventArgs e)
    {
        var item = ((View)sender).BindingContext as DataModel;
        if (item != null)
        {
            this.collectionView.EndItemSwipe(true);
            item.Visited = !item.Visited;
        }
    }
    // << collectionview-itemswipe-code
}