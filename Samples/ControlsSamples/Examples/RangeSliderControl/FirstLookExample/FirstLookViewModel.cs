using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Dispatching;
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
    private readonly ObservableCollection<RentalProperty> properties;

    private double priceRangeStart = 80;
    private double priceRangeEnd = 250;
    private double guestsNumber = 3;
    private IDispatcherTimer dispatcherTimer;

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
                this.ScheduleFilterItems();
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
                this.ScheduleFilterItems();
            }
        }
    }

    public double GuestsNumber
    {
        get => this.guestsNumber;
        set
        {
            if (this.UpdateValue(ref this.guestsNumber, value))
            {
                this.ScheduleFilterItems();
            }
        }
    }

    private void ScheduleFilterItems()
    {
        if (this.dispatcherTimer == null)
        {
            this.dispatcherTimer = Application.Current.Dispatcher.CreateTimer();
            this.dispatcherTimer.IsRepeating = false;
            this.dispatcherTimer.Interval = TimeSpan.FromSeconds(0.1);
            this.dispatcherTimer.Tick += (s, e) => this.FilterItems();
        }

        this.dispatcherTimer.Stop();
        this.dispatcherTimer.Start();
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
            this.guestsNumber <= guests;

        return passesFilter;
    }
}