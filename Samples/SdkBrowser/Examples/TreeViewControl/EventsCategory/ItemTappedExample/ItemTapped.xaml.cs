using Microsoft.Maui.Controls;

namespace SDKBrowserMaui.Examples.TreeViewControl.EventsCategory.ItemTappedExample;

public partial class ItemTapped : ContentView
{
	public ItemTapped()
	{
		InitializeComponent();
	}
	// >> treeview-itemtapped-event
    private void OnItemTapped(object sender, Telerik.Maui.Controls.ItemsView.ItemViewTappedEventArgs e)
    {
		var data = e.Item as Data;
		App.Current.MainPage.DisplayAlert("You have tapped on: "," " + data.Name,"OK");
    }
    // << treeview-itemtapped-event
}