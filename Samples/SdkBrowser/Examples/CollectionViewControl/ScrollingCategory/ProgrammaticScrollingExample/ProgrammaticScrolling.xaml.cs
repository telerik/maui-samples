using Microsoft.Maui.Controls;
using SDKBrowserMaui.Examples.CollectionViewControl;
using System.Collections.ObjectModel;
using System.Linq;

namespace SDKBrowserMaui.Examples.CollectionViewControl.ScrollingCategory.ProgrammaticScrollingExample;

public partial class ProgrammaticScrolling : ContentView
{
    public ProgrammaticScrolling()
    {
        InitializeComponent();
    }

    // >> collectionview-programmatic-scrolling
    private void OnScrollToLastItemClicked(object sender, System.EventArgs e)
    {
        var item = GetItemToScrollTo();
        this.collectionView.ScrollItemIntoView(item);
        this.scrollToLastItemBtn.Content = "Scrolled to: " + item.City;
        this.scrollToLastItemBtn.IsEnabled = false;
    }

    private DataModel GetItemToScrollTo()
    {
        return (collectionView.ItemsSource as ObservableCollection<DataModel>).LastOrDefault();
    }
    // << collectionview-programmatic-scrolling
}