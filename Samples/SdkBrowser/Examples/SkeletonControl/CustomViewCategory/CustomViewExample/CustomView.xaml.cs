using Microsoft.Maui.Controls;
using SDKBrowserMaui.Examples.CollectionViewControl;
namespace SDKBrowserMaui.Examples.SkeletonControl.CustomViewCategory.CustomViewExample;

public partial class CustomView : ContentView
{
	public CustomView()
	{
		InitializeComponent();
		this.BindingContext = new ViewModel();
	}
}