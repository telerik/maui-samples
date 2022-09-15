using QSF.Examples.DataGridControl.ColumnTypesExample;
using QSF.ViewModels;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Runtime.CompilerServices;
using Telerik.Maui.Controls.Compatibility.Common.Data;

namespace QSF.Examples.DataGridControl.GroupingExample;

public class GroupingViewModel : ConfigurationExampleViewModel
{
    private bool isGroupingAllowed = true;
    private ObservableCollection<Flight> flights;
    private bool isGroupedByAirline;
    private bool isGroupedByDate;
    private bool isGroupedByDirectFlight;
    private bool isGroupedByDestination;
    private ObservableCollection<GroupDescriptorBase> groupDescriptors;

    public GroupingViewModel()
    {
        this.Flights = FlightDataService.GetFlights();
    }

    public bool IsGroupingAllowed
    {
        get => this.isGroupingAllowed;
        set => this.UpdateValue(ref this.isGroupingAllowed, value);
    }

    public ObservableCollection<Flight> Flights
    {
        get => this.flights;
        set => this.UpdateValue(ref this.flights, value);
    }

    public bool IsGroupedByAirline
    {
        get => this.isGroupedByAirline;
        set => this.UpdateValue(ref this.isGroupedByAirline, value);
    }

    public bool IsGroupedByDate
    {
        get => this.isGroupedByDate;
        set => this.UpdateValue(ref this.isGroupedByDate, value);
    }

    public bool IsGroupedByDirectFlight
    {
        get => this.isGroupedByDirectFlight;
        set => this.UpdateValue(ref this.isGroupedByDirectFlight, value);
    }

    public bool IsGroupedByDestination
    {
        get => this.isGroupedByDestination;
        set => this.UpdateValue(ref this.isGroupedByDestination, value);
    }

    public ObservableCollection<GroupDescriptorBase> GroupDescriptors
    {
        get
        {
            return this.groupDescriptors;
        }
        set
        {
            ObservableCollection<GroupDescriptorBase> oldValue = this.groupDescriptors;

            if (this.UpdateValue(ref this.groupDescriptors, value))
            {
                this.OnGroupDescriptorsChanged(oldValue);
            }
        }
    }

    protected override void OnPropertyChanged([CallerMemberName] string propertyName = null)
    {
        base.OnPropertyChanged(propertyName);

        this.UpdateCorrespondingDescriptor(propertyName);
    }

    private void OnGroupDescriptorsChanged(ObservableCollection<GroupDescriptorBase> oldValue)
    {
        if (oldValue != null)
        {
            oldValue.CollectionChanged -= this.GroupDescriptors_CollectionChanged;
        }

        if (this.groupDescriptors != null)
        {
            this.groupDescriptors.CollectionChanged += this.GroupDescriptors_CollectionChanged;
        }

        this.UpdateFlags();
    }

    private void GroupDescriptors_CollectionChanged(object sender, NotifyCollectionChangedEventArgs args)
    {
        this.UpdateFlags();
    }

    private void UpdateCorrespondingDescriptor(string propertyName)
    {
        if (propertyName == nameof(this.IsGroupedByAirline))
        {
            this.EnsureDescriptor(nameof(Flight.AirlineName), this.IsGroupedByAirline);
        }
        else if (propertyName == nameof(this.IsGroupedByDate))
        {
            this.EnsureDescriptor(nameof(Flight.Date), this.IsGroupedByDate);
        }
        else if(propertyName == nameof(this.IsGroupedByDirectFlight))
        {
            this.EnsureDescriptor(nameof(Flight.IsDirect), this.IsGroupedByDirectFlight);
        }
        else if (propertyName == nameof(this.IsGroupedByDestination))
        {
            this.EnsureDescriptor(nameof(Flight.DestinationFullName), this.IsGroupedByDestination);
        }
    }

    private void EnsureDescriptor(string propertyName, bool include)
    {
        ObservableCollection<GroupDescriptorBase> descriptors = this.GroupDescriptors;

        if (descriptors == null)
        {
            return;
        }

        GroupDescriptorBase descriptor = this.TryGetDescriptor(propertyName);

        if (include)
        {
            if (descriptor == null)
            {
                descriptors.Add(new PropertyGroupDescriptor { PropertyName = propertyName });
            }
        }
        else
        {
            if (descriptor != null)
            {
                descriptors.Remove(descriptor);
            }
        }
    }

    private void UpdateFlags()
    {
        this.IsGroupedByAirline = this.TryGetDescriptor(nameof(Flight.AirlineName)) != null;
        this.IsGroupedByDate = this.TryGetDescriptor(nameof(Flight.Date)) != null;
        this.IsGroupedByDirectFlight = this.TryGetDescriptor(nameof(Flight.IsDirect)) != null;
        this.IsGroupedByDestination = this.TryGetDescriptor(nameof(Flight.DestinationFullName)) != null;
    }

    private GroupDescriptorBase TryGetDescriptor(string propertyName)
    {
        GroupDescriptorBase descriptor = this.groupDescriptors?.FirstOrDefault(d => (d as PropertyGroupDescriptor)?.PropertyName == propertyName);
        return descriptor;
    }
}
