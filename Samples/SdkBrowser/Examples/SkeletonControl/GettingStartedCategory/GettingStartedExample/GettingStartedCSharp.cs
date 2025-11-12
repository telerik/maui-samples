using Microsoft.Maui.Controls;
using Telerik.Maui.Controls;

namespace SDKBrowserMaui.Examples.SkeletonControl.GettingStartedCategory.GettingStartedExample;

public class GettingStartedCSharp : ContentView
{
	public GettingStartedCSharp()
	{
		// >> skeleton-getting-started-csharp
		var skeleton = new RadSkeleton();
		// << skeleton-getting-started-csharp
		this.Content = skeleton;
	}
}