using Microsoft.Maui.Controls;

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

    private void OnCollapseAllclicked(object sender, System.EventArgs e)
    {
        // >> treeview-collapse-all-method
        this.treeView.CollapseAll();
        // << treeview-collapse-all-method
    }
}