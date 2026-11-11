using System.Collections.ObjectModel;

namespace SDKBrowserMaui.Examples.CarouselControl;

// >> carousel-viewmodel
public class CarouselViewModel
{
    public ObservableCollection<CarouselItem> Items { get; } = new()
    {
        new CarouselItem
        {
            Title = "Blueberry Pancakes",
            Description = "Dutch baby pancake with fresh blueberries, orange zest, and a drizzle of maple syrup.",
            Image = "carousel_firstlook_1.jpg",
            IsNewDish = true
        },
        new CarouselItem
        {
            Title = "Bolognese",
            Description = "Slow-simmered ragù with hand-pulled spaghetti, topped with micro herbs.",
            Image = "carousel_firstlook_2.jpg",
            IsNewDish = false
        },
        new CarouselItem
        {
            Title = "Spaghetti",
            Description = "Fresh spaghetti with San Marzano tomato sauce and garden basil.",
            Image = "carousel_firstlook_3.jpg",
            IsNewDish = false
        },
        new CarouselItem
        {
            Title = "Lemon Herb Chicken",
            Description = "Herb-crusted chicken with lemon butter sauce and roasted vegetables.",
            Image = "carousel_firstlook_4.jpg",
            IsNewDish = true
        },
        new CarouselItem
        {
            Title = "Fine Dining",
            Description = "Delicate tartare with edible flowers, micro greens, and golden carrot purée.",
            Image = "carousel_firstlook_5.jpg",
            IsNewDish = false
        },
        new CarouselItem
        {
            Title = "Raspberry Cake",
            Description = "Raspberry coulis over a rich cheesecake with fresh berries.",
            Image = "carousel_firstlook_6.jpg",
            IsNewDish = true
        },
        new CarouselItem
        {
            Title = "Salmon Canapues",
            Description = "Smoked salmon on crispy toasts with cream cheese and dill.",
            Image = "carousel_firstlook_7.jpg",
            IsNewDish = false
        },
        new CarouselItem
        {
            Title = "Shrimp Fettuccine",
            Description = "Tiger prawns tossed with fettuccine in a light garlic cream sauce.",
            Image = "carousel_firstlook_8.jpg",
            IsNewDish = true
        }
    };
}
// << carousel-viewmodel
