﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Microsoft.Maui.Controls;
using QSF.ViewModels;
using QSF.Services;
using Telerik.AppUtils.Services;

namespace QSF.Examples.DataGridControl.ColumnTypesExample
{
    public class ColumnTypesViewModel : ExampleViewModel
    {
        private List<string> carriers;
        private List<(string, string)> countryNames;

        public ColumnTypesViewModel()
        {
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
            this.CreateFlights();
        }

        public ObservableCollection<Flight> Flights { get; set; }
        public ObservableCollection<string> Statuses { get; private set; }

        private void CreateFlights()
        {
            DateTime dateTime = new DateTime(2022, 7, 7, 12, 0, 0);

            var random = DependencyService
                .Get<ITestingService>()
                .Random(536334);

            for (int i = 0; i < this.carriers.Count; i++)
            {
                Flight flight = new Flight
                {
                    Id = $"OS{8008 + i}",
                    DestinationFullName = this.countryNames[i].Item1,
                    DestinationShortName = this.countryNames[i].Item2,
                    IsDirect = (i % 2) == 0,
                    AirlineName = this.carriers[i],
                    Terminal = random.Next(1,7),
                };
                dateTime = dateTime.AddDays(i);
                flight.Date = dateTime;
                dateTime = dateTime.AddHours(i + 2);
                flight.Time = dateTime;
                dateTime = dateTime.AddMinutes(12);
                flight.StatusTime = dateTime.ToString("HH:mm");
                flight.Status = this.Statuses[random.Next(this.Statuses.Count)];

                this.Flights.Add(flight);
            }
        }
    }
}
