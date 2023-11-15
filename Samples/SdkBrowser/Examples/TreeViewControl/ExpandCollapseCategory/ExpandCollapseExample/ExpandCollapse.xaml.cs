using Microsoft.Maui.Controls;
using SDKBrowserMaui.Converters;

namespace SDKBrowserMaui.Examples.TreeViewControl.ExpandCollapseCategory.ExpandCollapseExample;

public partial class ExpandCollapse : ContentView
{
	public ExpandCollapse()
	{
		InitializeComponent();
	}

    private void OnExpandAllClicked(object sender, System.EventArgs e)
    {
        // >> treeview-expand-all-method
        this.treeView.ExpandAll();
        // << treeview-expand-all-method
    }

    private void OnCollapseAllClicked(object sender, System.EventArgs e)
    {
        // >> treeview-collapse-all-method
        this.treeView.CollapseAll();
        // << treeview-collapse-all-method
    }
}