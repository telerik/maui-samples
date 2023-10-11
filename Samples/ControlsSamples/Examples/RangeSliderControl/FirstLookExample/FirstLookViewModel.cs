using System.Collections.Generic;
using System.Collections.ObjectModel;
using QSF.ViewModels;

namespace QSF.Examples.RangeSliderControl.FirstLookExample;

public class FirstLookViewModel : ExampleViewModel
{
    private static readonly List<string> propertyNames = new()
    {
        "Pampanga, Philippines",
        "Mykonos, Greece",
        "Rovinj, Croatia",
        "Paris, France",
        "Limbe, Cameroon",
        "Phuket, Thailand",
        "California, USA",
        "Piacenza, Italy",
        "San Blas, Panama",
        "California, USA",
        "California, USA",
        "Coles Bay, Australia"
    };

    private static readonly List<int> propertyMaxGuests = new() { 4, 2, 6, 2, 2, 8, 8, 4, 10, 6, 6, 5 };
    private static readonly List<double> propertyPrices = new() { 220, 130, 150, 160, 210, 250, 430, 120, 400, 680, 720, 430 };

    private readonly List<RentalProperty> source = new();

    private double priceRangeStart = 80;
    private double priceRangeEnd = 250;
    private double guestsNumberRangeEnd = 3;
    private readonly ObservableCollection<RentalProperty> properties;

    public FirstLookViewModel()
    {
        for (int i = 1; i <= 12; i++)
        {
            var maxGuestsCount = propertyMaxGuests[i-1];
            var bedroomsCount = maxGuestsCount / 2;

            this.source.Add(new RentalProperty()
            {
                Image = $"property_item_{i}.jpg",
                MaxGuests = maxGuestsCount,
                Name = propertyNames[i-1],
                Type = bedroomsCount + (bedroomsCount > 1 ? " bedrooms" : " bedroom"),
                PricePerNight = propertyPrices[i-1],
            });
        }

        this.properties = new ObservableCollection<RentalProperty>();

        this.FilterItems();
    }

    public ObservableCollection<RentalProperty> Properties { get => this.properties; }

    public double PriceRangeStart
    {
        get => this.priceRangeStart;
        set
        {
            if (this.UpdateValue(ref this.priceRangeStart, value))
            {
                this.FilterItems();
            }
        }
    }

    public double PriceRangeEnd
    {
        get => this.priceRangeEnd;
        set
        {
            if (this.UpdateValue(ref this.priceRangeEnd, value))
            {
                this.FilterItems();
            }
        }
    }

    public double GuestsNumberRangeEnd
    {
        get => this.guestsNumberRangeEnd;
        set
        {
            if (this.UpdateValue(ref this.guestsNumberRangeEnd, value))
            {
                this.FilterItems();
            }
        }
    }

    private void FilterItems()
    {
        int index = 0;

        for (int i = 0; i < this.source.Count; i++)
        {
            RentalProperty item = this.source[i];

            if (this.PassesFilter(item))
            {
                if (!this.properties.Contains(item))
                {
                    this.properties.Insert(index, item);
                }

                index++;
            }
            else
            {
                this.properties.Remove(item);
            }
        }
    }

    private bool PassesFilter(RentalProperty rental)
    {
        double price = rental.PricePerNight;
        double guests = rental.MaxGuests;

        bool passesFilter = this.priceRangeStart <= price && price <= this.priceRangeEnd &&
            this.guestsNumberRangeEnd <= guests;

        return passesFilter;
    }
}