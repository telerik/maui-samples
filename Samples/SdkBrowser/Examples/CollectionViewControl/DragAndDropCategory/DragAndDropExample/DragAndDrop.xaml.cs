using Microsoft.Maui.Controls;

namespace SDKBrowserMaui.Examples.CollectionViewControl.DragAndDropCategory.DragAndDropExample;

public partial class DragAndDrop : ContentView
{
	public DragAndDrop()
	{
		InitializeComponent();

		// >> collectionview-draganddrop-setvm
		this.BindingContext = new ViewModel();
		// << collectionview-draganddrop-setvm
	}
}