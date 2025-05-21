using QSF.Examples.DataGridControl.ColumnTypesExample;
using QSF.Examples.DataGridControl.GroupingExample;
using QSF.ViewModels;
using System.Collections.ObjectModel;

namespace QSF.Examples.CollectionViewControl.GroupingExample;

public class GroupingViewModel : ConfigurationExampleViewModel
{
    private ObservableCollection<Flight> flights;
    private bool enableStickyGroupHeaders = true;

    public GroupingViewModel()
    {
        this.Flights = FlightDataService.GetFlights();
    }

    public ObservableCollection<Flight> Flights
    {
        get => this.flights;
        set => this.UpdateValue(ref this.flights, value);
    }

    public bool EnableStickyGroupHeaders
    {
        get => this.enableStickyGroupHeaders;
        set => this.UpdateValue(ref this.enableStickyGroupHeaders, value);
    }
}
