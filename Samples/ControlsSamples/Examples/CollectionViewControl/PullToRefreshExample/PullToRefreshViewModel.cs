using Microsoft.Maui.Controls;
using QSF.Services;
using QSF.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Telerik.AppUtils.Services;

namespace QSF.Examples.CollectionViewControl.PullToRefreshExample;

public class PullToRefreshViewModel : ExampleViewModel
{
    private bool isRefreshing;
    private Random random;
    private List<string> descriptions;
    private List<string> images;
    private Dictionary<string, string> recipesData;

    public PullToRefreshViewModel()
    {
        this.random = DependencyService
            .Get<ITestingService>()
            .Random(6);

        this.descriptions = new List<string>
        {
            "These are the exact steps you need to follow to prepare the dish:",
            "This is the recipe:",
            "You want to prepare this dish. Follow the steps here!",
        };

        this.images = new List<string>
        {
            "\ue8ad",
            "\ue83f",
            "\ue8b0",
            "\ue8af"
        };

        this.recipesData = new Dictionary<string, string>
        {
            { "Fluffy Glazed Donuts", this.images[0] },
            { "Vanilla Ice Cream", this.images[1] },
            { "Fruit Salad With Honey Dressing", this.images[2] },
            { "Mojito recipe", this.images[3] },
            { "Summer Fruit Salad", this.images[2] },
        };

        this.Recipes = new ObservableCollection<Recipe>
        {
            new Recipe { Name = "Vegetarian Toast", Description = "A quick veggie-packed version of a classic Welsh rarebit served with soft runny eggs.", Icon = "\ue8ae" },
            new Recipe { Name = "Strawberry Cake", Description = "Warm cake with a crunchy buttery streusel topping.", Icon = "\ue83a" },
            new Recipe { Name = "Creamy Donuts", Description = "A light, buttery taste with a perfectly decadent glaze", Icon = "\ue8ad" },
            new Recipe { Name = "Red Sangria", Description = "Light and bubbly with an addition of seltzer water and the perfect ratio of wine to brandy.", Icon = "\ue8af" },
            new Recipe { Name = "Miso chickpeas and avocado on toast", Description = "Turn a storecupboard staple into a twist on this popular brunch dish, livened up with sesame seeds and spring onions.", Icon = "\ue8ae" },
            new Recipe { Name = "Chocolate ice cream", Description = "This chocolate ice cream not only tastes divine, it's easy to make.", Icon = "\ue83f" },
            new Recipe { Name = "Confit garlic on toast", Description = "Mornings in Spain often start with 'pan con tomate' (toast slathered with tomato and garlic purée). Use British tomatoes in this deconstructed version.", Icon = "\ue8ae" },
            new Recipe { Name = "Basque Cheesecake", Description = "With a mousse-like creamy vanilla texture and gorgeous golden “burnt” surface.", Icon = "\ue83a" },
        };

        this.RefreshCommand = new Command(async () =>
        {
            this.IsRefreshing = true;

            await Task.Delay(1000);
            this.CreateRecipes();

            this.IsRefreshing = false;
        });
    }

    public bool IsRefreshing
    {
        get => this.isRefreshing;
        set => this.UpdateValue(ref this.isRefreshing, value);
    }

    public ICommand RefreshCommand { get; set; }

    public ObservableCollection<Recipe> Recipes {get;set;}

    private void CreateRecipes()
    {
        var startIndex = this.Recipes.Count;
        var endIndex = this.Recipes.Count + 6;
        int i = 0;

        for (int itemIndex = startIndex; itemIndex < endIndex; itemIndex++)
        {
            string randomKey = this.recipesData.Keys.ElementAt(this.random.Next(0, this.recipesData.Count));
            string randomValue = this.recipesData[randomKey];

            Recipe recipe = new Recipe
            {
                Name = randomKey,
                Description = this.descriptions[this.random.Next(0, this.descriptions.Count)],
                Icon = randomValue,
            };

            this.Recipes.Insert(i, recipe);
            i++;
        }
    }
}
