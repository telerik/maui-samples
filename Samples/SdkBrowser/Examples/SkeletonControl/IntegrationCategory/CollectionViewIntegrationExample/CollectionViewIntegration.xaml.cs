using Microsoft.Maui.Controls;
using SDKBrowserMaui.Examples.AutoCompleteControl.ViewModels;

namespace SDKBrowserMaui.Examples.SkeletonControl.IntegrationCategory.CollectionViewIntegrationExample;

public partial class CollectionViewIntegration : ContentView
{
	public CollectionViewIntegration()
	{
		InitializeComponent();
		// >> skeleton-integration-collectionview-bindingcontext
		this.BindingContext = new ClientsViewModel();
		// << skeleton-integration-collectionview-bindingcontext
	}
}