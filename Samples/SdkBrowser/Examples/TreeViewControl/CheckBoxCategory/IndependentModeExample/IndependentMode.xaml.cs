using Microsoft.Maui.Controls;

namespace SDKBrowserMaui.Examples.TreeViewControl.CheckBoxCategory.IndependentModeExample;

public partial class IndependentMode : ContentView
{
	public IndependentMode()
	{
		InitializeComponent();

		this.treeView.ExpandAll();
	}
}