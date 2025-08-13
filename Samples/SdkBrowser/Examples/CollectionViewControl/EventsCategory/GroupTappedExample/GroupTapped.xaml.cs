using Microsoft.Maui.Controls;

namespace SDKBrowserMaui.Examples.CollectionViewControl.EventsCategory.GroupTappedExample;

public partial class GroupTapped : ContentView
{
    public GroupTapped()
    {
        InitializeComponent();
    }

    // >> collectionview-group-tapped-event
    private void OnGroupTapped(object sender, Telerik.Maui.RadTappedEventArgs<Telerik.Maui.Controls.CollectionView.GroupContext> e)
    {
        Application.Current.Windows[0].Page.DisplayAlert("", "You have tapped on Group: " + e.Data.Key, "OK");
    }
    // << collectionview-group-tapped-event
}