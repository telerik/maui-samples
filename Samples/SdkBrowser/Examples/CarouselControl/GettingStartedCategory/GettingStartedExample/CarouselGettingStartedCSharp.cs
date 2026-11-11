using Microsoft.Maui.Controls;
using Microsoft.Maui;
using Telerik.Maui.Controls;

namespace SDKBrowserMaui.Examples.CarouselControl.GettingStartedCategory.GettingStartedExample;

public class CarouselGettingStartedCSharp : ContentView
{
    public CarouselGettingStartedCSharp()
    {
        // >> carousel-getting-started-csharp
        var carousel = new RadCarousel
        {
            VerticalOptions = LayoutOptions.Center,
            ItemsSource = new CarouselViewModel().Items,
            ItemTemplate = new DataTemplate(() =>
            {
                var image = new Image { Aspect = Aspect.AspectFill };
                image.SetBinding(Image.SourceProperty, nameof(CarouselItem.Image));
                return image;
            })
        };
        // << carousel-getting-started-csharp

        this.Content = carousel;
    }
}
