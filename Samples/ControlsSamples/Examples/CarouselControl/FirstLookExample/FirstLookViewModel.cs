using QSF.ViewModels;
using System.Collections.ObjectModel;

namespace QSF.Examples.CarouselControl.FirstLookExample;

public class FirstLookViewModel : ExampleViewModel
{
    public ObservableCollection<Dish> Dishes { get; } = new()
    {
        new Dish
        {
            Category = "BREAKFAST",
            Name = "Blueberry Pancakes",
            Price = 14,
            Rating = 4.8,
            Description = "Dutch baby pancake with fresh blueberries, orange zest, and a drizzle of maple syrup.",
            Image = "carousel_firstlook_1.jpg"
        },
        new Dish
        {
            Category = "LUNCH",
            Name = "Bolognese",
            Price = 22,
            Rating = 4.9,
            Description = "Slow-simmered ragù with hand-pulled spaghetti, topped with micro herbs.",
            Image = "carousel_firstlook_2.jpg"
        },
        new Dish
        {
            Category = "LUNCH",
            Name = "Spaghetti",
            Price = 18,
            Rating = 4.7,
            Description = "Fresh spaghetti with San Marzano tomato sauce and garden basil.",
            Image = "carousel_firstlook_3.jpg"
        },
        new Dish
        {
            Category = "DINNER",
            Name = "Lemon Herb Chicken",
            Price = 26,
            Rating = 4.8,
            Description = "Herb-crusted chicken with lemon butter sauce and roasted vegetables.",
            Image = "carousel_firstlook_4.jpg"
        },
        new Dish
        {
            Category = "FINE DINING",
            Name = "Fine Dining",
            Price = 58,
            Rating = 5.0,
            Description = "Delicate tartare with edible flowers, micro greens, and golden carrot purée.",
            Image = "carousel_firstlook_5.jpg"
        },
        new Dish
        {
            Category = "DESSERT",
            Name = "Raspberry Cake",
            Price = 16,
            Rating = 4.6,
            Description = "Raspberry coulis over a rich cheesecake with fresh berries.",
            Image = "carousel_firstlook_6.jpg"
        },
        new Dish
        {
            Category = "DINNER",
            Name = "Salmon Canapés",
            Price = 28,
            Rating = 4.9,
            Description = "Smoked salmon on crispy toasts with cream cheese and dill.",
            Image = "carousel_firstlook_7.jpg"
        },
        new Dish
        {
            Category = "DINNER",
            Name = "Shrimp Fettuccine",
            Price = 24,
            Rating = 4.8,
            Description = "Tiger prawns tossed with fettuccine in a light garlic cream sauce.",
            Image = "carousel_firstlook_8.jpg"
        }
    };
}
