using Microsoft.Maui.Controls;

namespace SDKBrowserMaui.Examples.CollectionViewControl.DragAndDropCategory.ReorderWithGroupingExample;

public partial class ReorderWithGrouping : ContentView
{
	public ReorderWithGrouping()
	{
		InitializeComponent();

		// >> collectionview-reorderwithgroups-setvm
		this.BindingContext = new ViewModel();
		// << collectionview-reorderwithgroups-setvm
	}
}