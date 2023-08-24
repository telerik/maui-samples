using Microsoft.Maui.Controls;

namespace SDKBrowserMaui.Examples.TreeViewControl.CheckBoxCategory.RecursiveModeExample;

public partial class RecursiveMode : ContentView
{
	public RecursiveMode()
	{
		InitializeComponent();

		this.treeView.ExpandAll();
	}
}