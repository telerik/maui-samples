using Microsoft.Maui.Controls;
using Telerik.Maui.Controls;

namespace SDKBrowserMaui.Examples.CircularSliderControl.GettingStartedCategory.GettingStartedExample;

public class CircularSliderGettingStartedCSharp : ContentView
{
    public CircularSliderGettingStartedCSharp()
    {
        var content = new Grid();

        // >> circularslider-getting-started-csharp
        var circularSlider = new RadCircularSlider();
        circularSlider.Minimum = 0;
        circularSlider.Maximum = 100;
        circularSlider.Value = 35;
        circularSlider.MinimumHeightRequest = 300;
        circularSlider.HorizontalOptions = LayoutOptions.Fill;
        // << circularslider-getting-started-csharp

#if MACCATALYST || WINDOWS
        circularSlider.VerticalOptions = LayoutOptions.Start;
#else
        circularSlider.VerticalOptions = LayoutOptions.Fill;
#endif
        content.Add(circularSlider);
        this.Content = content;
    }
}
