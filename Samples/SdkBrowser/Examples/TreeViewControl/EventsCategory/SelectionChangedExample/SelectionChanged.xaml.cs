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
		App.Current.MainPage.DisplayAlert("Selection has changed ","","OK");
    }
    // << treeview-selectionchanged-event
}