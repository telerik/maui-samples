using Microsoft.Maui.Controls;
using System.Collections.ObjectModel;
using System.Linq;

namespace SDKBrowserMaui.Examples.TreeViewControl.ScrollingCategory.ProgrammaticScrollingExample;

public partial class ProgrammaticScrolling : ContentView
{
	public ProgrammaticScrolling()
	{
		InitializeComponent();

        this.treeView.ExpandAll();
	}

    // >> treeview-programmatic-scrolling
    private void Button_Clicked(object sender, System.EventArgs e)
    {
        var item = GetItemToScroll();
        this.treeView.ScrollTo(item);
        this.scrollBtn.Text="Scrolled to: " + item.Name;
        this.scrollBtn.IsEnabled=false;
    }

    private Country GetItemToScroll()
    {
        return (treeView.ItemsSource as ObservableCollection<Country>).LastOrDefault();
    }
    // << treeview-programmatic-scrolling
}