using Microsoft.Maui.Controls;
using Telerik.Maui.Controls;

namespace SDKBrowserMaui.Examples.TreeViewControl.EventsCategory.SelectionChangedExample;

public partial class SelectionChanged : ContentView
{
	public SelectionChanged()
	{
		InitializeComponent();
	}
	// >> treeview-selectionchanged-event
    private void OnSelectionChanged(object sender, System.EventArgs e)
    {
		if (this.treeView.SelectedItem == null) 
		{
			return;
		}

		var selectedItem = this.treeView.SelectedItem.ToString();
		App.Current.MainPage.DisplayAlert("Selection has changed ", "Selected item is: " + selectedItem, "OK");
    }
    // << treeview-selectionchanged-event
}