using Microsoft.Maui.Controls;

namespace SDKBrowserMaui.Examples.TreeDataGridControl.GettingStartedCategory.GettingStartedExample;

public partial class GettingStartedXaml : ContentView
{
	public GettingStartedXaml()
	{
		InitializeComponent();

        this.treeDataGrid.AutomationId = "treeDataGrid";
    }
}