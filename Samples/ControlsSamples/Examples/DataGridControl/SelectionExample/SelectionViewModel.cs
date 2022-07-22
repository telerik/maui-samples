using QSF.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Telerik.Maui.Controls.Compatibility.DataGrid;

namespace QSF.Examples.DataGridControl.SelectionExample;

public class SelectionViewModel : ConfigurationExampleViewModel
{
    private List<string> carriers;
    private List<(string, string)> countryNames;
    private DataGridSelectionMode selectedMode;
    private DataGridSelectionUnit selectedUnit;

    public SelectionViewModel()
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
            ("Amsterdam", "Ams")
        };

        this.Flights = new ObservableCollection<Flight>();
        this.CreateFlights();

        this.SelectedMode = DataGridSelectionMode.Single;
        this.SelectedUnit = DataGridSelectionUnit.Cell;
    }

    public ObservableCollection<Flight> Flights { get; set; }

    public IEnumerable<DataGridSelectionMode> SelectionModes
    {
        get => (IEnumerable<DataGridSelectionMode>)Enum.GetValues(typeof(DataGridSelectionMode));
    }

    public IEnumerable<DataGridSelectionUnit> SelectionUnits
    {
        get => (IEnumerable<DataGridSelectionUnit>)Enum.GetValues(typeof(DataGridSelectionUnit));
    }

    public DataGridSelectionMode SelectedMode
    {
        get => this.selectedMode;
        set => this.UpdateValue(ref this.selectedMode, value);
    }

    public DataGridSelectionUnit SelectedUnit
    {
        get => this.selectedUnit;
        set => this.UpdateValue(ref this.selectedUnit, value);
    }

    private void CreateFlights()
    {
        DateTime dateTime = new DateTime(2022, 7, 7, 12, 0, 0);

        for (int i = 0; i < this.carriers.Count; i++)
        {
            Flight flight = new Flight
            {
                Id = $"OS{8008 + i}",
                DestinationFullName = this.countryNames[i].Item1,
                DestinationShortName = this.countryNames[i].Item2,
                IsDirect = (i % 2) == 0,
                AirlineName = this.carriers[i],
            };

            dateTime = dateTime.AddDays(i).AddHours(i + 2).AddMinutes(12);
            flight.DateTime = dateTime;
            flight.Status = $"Departed {dateTime.ToString("HH:mm")}";

            this.Flights.Add(flight);
        }
    }
}
