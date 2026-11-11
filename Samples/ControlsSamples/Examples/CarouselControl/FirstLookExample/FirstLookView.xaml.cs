using Microsoft.Maui.Devices;
using Telerik.Maui.Controls;

namespace QSF.Examples.CarouselControl.FirstLookExample;

public partial class FirstLookView : RadContentView
{
    public FirstLookView()
    {
        InitializeComponent();
    }

    private void OnCarouselHostSizeChanged(object sender, EventArgs e)
    {
        if (DeviceInfo.Idiom == DeviceIdiom.Phone)
            return;

        double width = CarouselHost.Width;

        if (width <= 0)
            return;

        double height;
        double peekArea;

        if (width >= 1000)
        {
            height = 520;
            peekArea = 0.70;
        }
        else if (width >= 780)
        {
            height = 500;
            peekArea = 0.60;
        }
        else
        {
            height = 460;
            peekArea = 0.45;
        }

        if (Math.Abs(CarouselHost.HeightRequest - height) > 1)
            CarouselHost.HeightRequest = height;

        if (Math.Abs(DishesCarousel.PeekAreaRatio - peekArea) > 0.001)
            DishesCarousel.PeekAreaRatio = peekArea;
    }
}
