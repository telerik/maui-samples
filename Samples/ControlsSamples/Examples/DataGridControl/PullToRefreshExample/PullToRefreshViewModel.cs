using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Microsoft.Maui.Controls;
using QSF.Examples.DataGridControl.ColumnTypesExample;
using QSF.Services;
using QSF.ViewModels;
using Telerik.AppUtils.Services;

namespace QSF.Examples.DataGridControl.PullToRefreshExample;

public class PullToRefreshViewModel : ExampleViewModel
{
    private bool isRefreshing;
    private int flightNumber = 8008;
    private DateTime dateTime = new DateTime(2026, 7, 7, 12, 0, 0);
    private Random random;
    private List<string> carriers;
    private List<(string, string)> countryNames;

    public PullToRefreshViewModel()
    {
        this.random = DependencyService
            .Get<ITestingService>()
            .Random(536334);

        this.carriers = new List<string>
        {
            "SkyRocket Airways",
            "Amazing Airlines",
            "Save Wings Airways",
            "Silver Lightning Airline",
            "Butterfly Airways",
            "Skyline Air",
            "Air Comfort",
            "Austrian Airlines"
        };

        this.countryNames = new List<(string, string)>
        {
            ("Vienna", "Vie"),
            ("Sofia", "Sof"),
            ("Rome", "Rom"),
            ("Istanbul", "Ist"),
            ("Paris", "Pa"),
            ("Berlin", "Ber"),
            ("London", "Ldn"),
            ("Tokyo", "Tyo")
        };

        this.Statuses = new ObservableCollection<string>
        {
            "Departed",
            "Boarding",
            "Check-in",
            "Delayed",
        };

        this.Flights = new ObservableCollection<Flight>();
        this.CreateFlights(this.carriers.Count);

        this.RefreshCommand = new Command(async () =>
        {
            this.IsRefreshing = true;

            await Task.Delay(1000);
            this.CreateFlights(4);

            this.IsRefreshing = false;
        });
    }

    public bool IsRefreshing
    {
        get => this.isRefreshing;
        set => this.UpdateValue(ref this.isRefreshing, value);
    }

    public ICommand RefreshCommand { get; set; }

    public ObservableCollection<Flight> Flights { get; set; }

    public ObservableCollection<string> Statuses { get; private set; }

    private void CreateFlights(int count)
    {
        for (int i = 0; i < count; i++)
        {
            int index = this.random.Next(this.carriers.Count);

            Flight flight = new Flight
            {
                Id = $"OS{this.flightNumber++}",
                DestinationFullName = this.countryNames[index].Item1,
                DestinationShortName = this.countryNames[index].Item2,
                IsDirect = (index % 2) == 0,
                AirlineName = this.carriers[index],
                Terminal = this.random.Next(1, 7),
            };

            this.dateTime = this.dateTime.AddHours(this.random.Next(1, 5));
            flight.Date = this.dateTime;
            flight.Day = "Today";
            flight.Time = this.dateTime;
            flight.StatusTime = this.dateTime.AddMinutes(12).ToString("HH:mm");
            flight.Status = this.Statuses[this.random.Next(this.Statuses.Count)];

            this.Flights.Insert(i, flight);
        }
    }
}
