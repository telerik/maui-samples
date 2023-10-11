using Microsoft.Maui.Controls;
using Telerik.Maui.Controls;
using Telerik.Maui.Controls.NavigationView;

namespace SDKBrowserMaui.Examples.NavigationViewControl.EventsCategory.EventsExample;

public partial class Events : ContentView
{
	public Events()
	{
		InitializeComponent();
	}

    // >> navigationview-events-selectionchanged
    private void OnSelectionChanged(object sender, System.EventArgs e)
    {
        var control = (RadNavigationView)sender;
        var selectedItem = (NavigationViewItem)control.SelectedItem;
        this.label.Text = selectedItem.Text;
    }
    // << navigationview-events-selectionchanged

    // >> navigationview-events-itemclicked
    private void OnItemClicked(object sender, Telerik.Maui.Controls.NavigationView.NavigationViewItemEventArgs e)
    {
        var item = e.NavigationItem;
        Application.Current.MainPage.DisplayAlert("Item " + item.Text, " is clicked", "OK");
    }
    // << navigationview-events-itemclicked

    // >> navigationview-events-paneopened
    private void OnPaneOpened(object sender, System.EventArgs e)
    {
        Application.Current.MainPage.DisplayAlert("Pane is opened", "", "OK");
    }
    // << navigationview-events-paneopened

    // >> navigationview-events-paneclosed
    private void OnPaneClosed(object sender, System.EventArgs e)
    {
        Application.Current.MainPage.DisplayAlert("Pane is closed", "", "OK");
    }
    // << navigationview-events-paneclosed
}