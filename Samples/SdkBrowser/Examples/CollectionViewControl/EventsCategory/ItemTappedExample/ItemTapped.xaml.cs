using Microsoft.Maui.Controls;

namespace SDKBrowserMaui.Examples.CollectionViewControl.EventsCategory.ItemTappedExample;

public partial class ItemTapped : ContentView
{
    public ItemTapped()
    {
        InitializeComponent();
    }

    // >> collectionview-item-tapped-event
    private void OnItemTapped(object sender, Telerik.Maui.RadTappedEventArgs<object> e)
    {
        var data = e.Data as DataModel;
        Application.Current.Windows[0].Page.DisplayAlert("", "You have tapped on " + data.City + " located in " + data.Country, "OK");
    }
    // << collectionview-item-tapped-event
}