using Microsoft.Maui.Controls;
using Telerik.Maui.Controls;

namespace SDKBrowserMaui.Examples.PromptInputControl.GettingStartedCategory.GettingStartedExample;

public class GettingStartedCSharp : ContentView
{
    public GettingStartedCSharp()
    {
        var stack = new VerticalStackLayout();
        // >> promptinput-gettingstarted-csharp
        var promptInput = new RadPromptInput();
        // << promptinput-gettingstarted-csharp
        stack.Children.Add(promptInput);
        this.Content = stack;
    }
}