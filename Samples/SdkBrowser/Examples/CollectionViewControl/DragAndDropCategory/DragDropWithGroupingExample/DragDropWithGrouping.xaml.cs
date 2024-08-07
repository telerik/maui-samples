using Microsoft.Maui.Controls;

namespace SDKBrowserMaui.Examples.CollectionViewControl.DragAndDropCategory.DragDropWithGroupingExample;

public partial class DragDropWithGrouping : ContentView
{
	public DragDropWithGrouping()
	{
		InitializeComponent();

		// >> collectionview-dragdroprwithgroups-setvm
		this.BindingContext = new ViewModel();
        // << collectionview-dragdroprwithgroups-setvm
    }
}