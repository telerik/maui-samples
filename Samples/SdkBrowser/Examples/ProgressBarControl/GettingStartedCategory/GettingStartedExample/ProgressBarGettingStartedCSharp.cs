using Microsoft.Maui.Controls;
using Telerik.Maui.Controls;

namespace SDKBrowserMaui.Examples.ProgressBarControl.GettingStartedCategory.GettingStartedExample;

public class ProgressBarGettingStartedCSharp : ContentView
{
    public ProgressBarGettingStartedCSharp()
    {
        // >> progressbar-getting-started-csharp
        var progressBarr = new RadLinearProgressBar()
        {
            Value = 25
        };
        // <<  progressbar-getting-started-csharp

        var stack = new VerticalStackLayout();
        stack.Children.Add(progressBarr);
        Content = stack;
    }
}
