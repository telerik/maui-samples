using Microsoft.Maui.Controls;
using Telerik.Maui.Controls;

namespace SDKBrowserMaui.Examples.TemplatedButtonControl.GettingStartedCategory.GettingStartedExample;

public class GettingStartedCSharp : ContentView
{
	public GettingStartedCSharp()
	{
		var stack = new VerticalStackLayout();
		stack.HorizontalOptions = LayoutOptions.Center;
		// >> templatedbutton-gettingstarted-csharp
		var templatedButton = new RadTemplatedButton();
		templatedButton.Content = "My TemplatedButton Content";
        // << templatedbutton-gettingstarted-csharp
        stack.Children.Add(templatedButton);
		this.Content = stack;
	}
}