using QSF.Examples.DataGridControl.ColumnTypesExample;
using QSF.Examples.DataGridControl.GroupingExample;
using QSF.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QSF.Examples.CollectionViewControl.GroupingExample;

public class GroupingViewModel : ExampleViewModel
{
    private ObservableCollection<Flight> flights;

    public GroupingViewModel()
    {
        this.Flights = FlightDataService.GetFlights();
    }

    public ObservableCollection<Flight> Flights
    {
        get => this.flights;
        set => this.UpdateValue(ref this.flights, value);
    }
}
