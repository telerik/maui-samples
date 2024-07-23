using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Dispatching;
using QSF.ViewModels;

namespace QSF.Examples.CollectionViewControl.GridLayoutExample;

public class GridLayoutViewModel : ExampleViewModel
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
    private readonly ObservableCollection<RentalProperty> properties = new ObservableCollection<RentalProperty>();

    private double priceRangeStart = 80;
    private double priceRangeEnd = 250;
    private double guestsNumber = 3;

    public GridLayoutViewModel()
    {
        for (int i = 1; i <= 12; i++)
        {
            var maxGuestsCount = propertyMaxGuests[i-1];
            var bedroomsCount = maxGuestsCount / 2;

            this.properties.Add(new RentalProperty()
            {
                Image = $"property_item_{i}.jpg",
                MaxGuests = maxGuestsCount,
                Name = propertyNames[i-1],
                Type = bedroomsCount + (bedroomsCount > 1 ? " bedrooms" : " bedroom"),
                PricePerNight = propertyPrices[i-1],
            });
        }
    }

    public ObservableCollection<RentalProperty> Properties { get => this.properties; }

    public double PriceRangeStart
    {
        get => this.priceRangeStart;
        set => this.UpdateValue(ref this.priceRangeStart, value);
    }

    public double PriceRangeEnd
    {
        get => this.priceRangeEnd;
        set => this.UpdateValue(ref this.priceRangeEnd, value);
    }

    public double GuestsNumber
    {
        get => this.guestsNumber;
        set => this.UpdateValue(ref this.guestsNumber, value);
    }
}