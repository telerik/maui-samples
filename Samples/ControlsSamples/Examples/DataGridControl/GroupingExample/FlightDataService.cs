using QSF.Examples.DataGridControl.ColumnTypesExample;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace QSF.Examples.DataGridControl.GroupingExample;

public class FlightDataService
{
    public static ObservableCollection<Flight> GetFlights()
    {
        List<string> carriers = new List<string>
        {
            "SkyRocket Airways",
            "Amazing Airlines",
            "Save Wings Airways",
            "Silver Lightning Airline",
            "Butterfly Airways",
            "Skyline Air",
            "Air Comfort",
            "Austrian Airlines",
        };

        List<string> statuses = new List<string>
        {
            "Departed",
            "Boarding",
            "Check-in",
            "Delayed",
        };

        List<(string, string)> destinationNames = new List<(string, string)>
        {
            ("Vienna", "Vie"),
            ("Sofia", "Sof"),
            ("Rome", "Rom"),
            ("Istanbul", "Ist"),
            ("Paris", "Pa"),
            ("Berlin", "Ber"),
            ("London", "Ldn"),
            ("Tokyo", "Tyo"),
        };

        List<DateTime> times = new List<DateTime>()
        {
            DateTime.Now,
            DateTime.Now.AddMinutes(15),
            DateTime.Now.AddMinutes(44),
        };

        return CreateFlights(carriers, statuses, destinationNames, times);
    }

    private static ObservableCollection<Flight> CreateFlights(List<string> carriers, List<string> statuses, List<(string, string)> destinationNames, List<DateTime> times)
    {
        ObservableCollection<Flight> flights = new ObservableCollection<Flight>();
        Random r = new Random();

        for (int i = 0; i < 200; i++)
        {
            var destination = destinationNames[r.Next(0, destinationNames.Count)];

            Flight flight = new Flight
            {
                Id = $"OS{8508 + i}",
                DestinationFullName = destination.Item1,
                DestinationShortName = destination.Item2,
                IsDirect = (i % 2) == 0,
                AirlineName = carriers[r.Next(0, carriers.Count)],
                Terminal = r.Next(1, 7),
            };

            DateTime dateTime = times[r.Next(0, times.Count)];
            dateTime = dateTime.AddHours(i / 5);
            flight.Date = dateTime;            
            dateTime = dateTime.AddHours(i + 3);
            flight.Time = dateTime;            
            dateTime = dateTime.AddMinutes(12);
            flight.StatusTime = dateTime.ToString("HH:mm");

            flight.Status = statuses[r.Next(0, statuses.Count)];

            flights.Add(flight);
        }

        return flights;
    }
}
