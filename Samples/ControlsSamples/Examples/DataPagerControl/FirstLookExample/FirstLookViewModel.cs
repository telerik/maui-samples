using QSF.Examples.DataGridControl.ColumnTypesExample;
using QSF.Examples.DataGridControl.GroupingExample;
using QSF.ViewModels;
using System.Collections.ObjectModel;

namespace QSF.Examples.DataPagerControl.FirstLookExample;

public class FirstLookViewModel : ExampleViewModel
{
    private ObservableCollection<Flight> flights;
    private ObservableCollection<int> pageSizes;
    private int pageSize = 25;

    public FirstLookViewModel()
    {
        this.Flights = FlightDataService.GetFlights();
        this.pageSizes = new ObservableCollection<int> { 5, 10, 15, 20, 25, 30, 35, 40, 45, 50 };
    }

    public ObservableCollection<Flight> Flights
    {
        get => this.flights;
        set => this.UpdateValue(ref this.flights, value);
    }

    public ObservableCollection<int> PageSizes { get => this.pageSizes; }

    public int PageSize
    {
        get => this.pageSize;
        set => this.UpdateValue(ref this.pageSize, value);
    }
}