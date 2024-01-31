using Microsoft.Maui.Controls;
using Telerik.Maui.Controls;

namespace SDKBrowserMaui.Examples.SliderControl.GettingStartedCategory.GettingStartedExample;

public class SliderGettingStartedCSharp : ContentView
{
	public SliderGettingStartedCSharp()
	{
        var content = new Grid();

        // >> slider-gettingstarted-csharp
        var slider = new RadSlider();
        slider.Minimum = 0;
        slider.Maximum = 100;
        slider.Value = 35;
        // << slider-gettingstarted-csharp

#if MACCATALYST || WINDOWS
        slider.WidthRequest = 800;
        slider.HorizontalOptions = LayoutOptions.Center;
        slider.Margin = new Microsoft.Maui.Thickness() { Top = 40 };
#endif

        content.Add(slider);
        this.Content = content;
    }
}