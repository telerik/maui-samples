using Microsoft.Maui.Controls;

namespace QSF.Examples.CollectionViewControl.ItemSwipeExample;

public partial class ItemSwipeView : ContentView
{
	public ItemSwipeView()
	{
		InitializeComponent();
	}

    private void FavouriteItemTapped(object sender, TappedEventArgs e)
    {
        var item = ((View)sender).BindingContext as MailItem;
        if (item != null)
        {
            this.collectionView.EndItemSwipe(true);
            item.IsFavourite = !item.IsFavourite;
        }
    }

    private void DeleteItemTapped(object sender, TappedEventArgs e)
    {
        var item = ((View)sender).BindingContext as MailItem;
        if (item != null)
        {
            this.collectionView.EndItemSwipe(true, animationFinished: () =>
            {
                (this.BindingContext as ItemSwipeViewModel).Mails.Remove(item);
            });
        }
    }
}