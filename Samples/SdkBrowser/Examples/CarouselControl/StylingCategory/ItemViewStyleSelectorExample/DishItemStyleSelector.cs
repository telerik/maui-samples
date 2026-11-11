using System;
using Microsoft.Maui.Controls;
using Telerik.Maui.Controls;

namespace SDKBrowserMaui.Examples.CarouselControl.StylingCategory.ItemViewStyleSelectorExample;

// >> carousel-item-view-style-selector-class
public class DishItemStyleSelector : IStyleSelector
{
    public Style BlueberryPancakesStyle { get; set; }

    public Style BologneseStyle { get; set; }

    public Style SpaghettiStyle { get; set; }

    public Style LemonHerbChickenStyle { get; set; }

    public Style FineDiningStyle { get; set; }

    public Style RaspberryCakeStyle { get; set; }

    public Style SalmonCanapesStyle { get; set; }

    public Style ShrimpFettuccineStyle { get; set; }

    public Style SelectStyle(object item, BindableObject bindable)
    {
        return item switch
        {
            CarouselItem { Image: "carousel_firstlook_1.jpg" } => this.BlueberryPancakesStyle,
            CarouselItem { Image: "carousel_firstlook_2.jpg" } => this.BologneseStyle,
            CarouselItem { Image: "carousel_firstlook_3.jpg" } => this.SpaghettiStyle,
            CarouselItem { Image: "carousel_firstlook_4.jpg" } => this.LemonHerbChickenStyle,
            CarouselItem { Image: "carousel_firstlook_5.jpg" } => this.FineDiningStyle,
            CarouselItem { Image: "carousel_firstlook_6.jpg" } => this.RaspberryCakeStyle,
            CarouselItem { Image: "carousel_firstlook_7.jpg" } => this.SalmonCanapesStyle,
            CarouselItem { Image: "carousel_firstlook_8.jpg" } => this.ShrimpFettuccineStyle,
            _ => throw new ArgumentException("The item must have a supported carousel image.", nameof(item))
        };
    }
}
// << carousel-item-view-style-selector-class
