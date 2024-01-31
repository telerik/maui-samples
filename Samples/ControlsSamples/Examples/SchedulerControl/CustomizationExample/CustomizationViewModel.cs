using QSF.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Telerik.Maui.Controls.Scheduler;

namespace QSF.Examples.SchedulerControl.CustomizationExample;

public class CustomizationViewModel : ExampleViewModel
{
    public static List<string> DefaultLocations = new List<string>() { "Sofia 501", "Sofia 502", "Sofia 503" };

    private static DateTime currentDate = DateTime.Today;

    private ObservableCollection<OfficeMeeting> appointmentsSource;

    private bool isRoom501Checked;
    private bool isRoom502Checked;
    private bool isRoom503Checked;

    private bool isInternalChange;

    public CustomizationViewModel()
    {
        this.appointmentsSource = new ObservableCollection<OfficeMeeting>()
        {
            new OfficeMeeting()
            {
                Subject = ".NET MAUI Weekly Planning",
                Start = currentDate.AddHours(15),
                End = currentDate.AddHours(16),
                Location = DefaultLocations[0]
            },

            new OfficeMeeting()
            {
                Subject = "Retrospective Meeting",
                Start = currentDate.AddHours(15),
                End = currentDate.AddHours(16),
                Location = DefaultLocations[1]
            },

            new OfficeMeeting()
            {
                Subject = "All Hands Meeting",
                Start = currentDate.AddHours(16).AddMinutes(30),
                End = currentDate.AddHours(17).AddMinutes(30),
                Location = DefaultLocations[2]
            }
        };

        foreach (OfficeMeeting appointment in this.appointmentsSource)
        {
            appointment.PropertyChanged += this.OnAppointmentPropertyChanged;
        }

        this.isRoom501Checked = true;
        this.isRoom502Checked = true;
        this.isRoom503Checked = true;

        this.FilteredAppointmentsSource.CollectionChanged += this.OnFilteredAppointmentsSourceCollectionChanged;

        this.UpdateVisibleAppointments();
    }

    public ObservableCollection<OfficeMeeting> FilteredAppointmentsSource { get; set; } = new ObservableCollection<OfficeMeeting>();

    public bool IsRoom501Checked
    {
        get => this.isRoom501Checked;
        set
        {
            if (this.UpdateValue(ref this.isRoom501Checked, value))
            {
                this.UpdateVisibleAppointments();
            }
        }
    }

    public bool IsRoom502Checked
    {
        get => this.isRoom502Checked;
        set
        {
            if (this.UpdateValue(ref this.isRoom502Checked, value))
            {
                this.UpdateVisibleAppointments();
            }
        }
    }

    public bool IsRoom503Checked
    {
        get => this.isRoom503Checked;
        set
        {
            if (this.UpdateValue(ref this.isRoom503Checked, value))
            {
                this.UpdateVisibleAppointments();
            }
        }
    }

    private void UpdateVisibleAppointments()
    {
        var locations = this.GetAvailableLocations();

        this.isInternalChange = true;

        this.FilteredAppointmentsSource.Clear();
        var newAppointments = this.appointmentsSource.Where(x => locations.Contains(x.Location));

        foreach (var appointment in newAppointments)
        {
            this.FilteredAppointmentsSource.Add(appointment);
        }

        this.isInternalChange = false;
    }

    private void OnFilteredAppointmentsSourceCollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
    {
        if (this.isInternalChange)
        {
            return;
        }

        switch (e.Action)
        {
            case System.Collections.Specialized.NotifyCollectionChangedAction.Add:
                var newAppointment = (OfficeMeeting)e.NewItems[0];
                newAppointment.PropertyChanged += this.OnAppointmentPropertyChanged;
                this.appointmentsSource.Add(newAppointment);
                var locations = this.GetAvailableLocations();
                if (!locations.Contains(newAppointment.Location))
                {
                    App.Current.Dispatcher.Dispatch(() =>
                    {
                        this.isInternalChange = true;
                        this.FilteredAppointmentsSource.Remove(newAppointment);
                        this.isInternalChange = false;
                    });
                }
                break;
            case System.Collections.Specialized.NotifyCollectionChangedAction.Remove:
                var removedAppointment = (OfficeMeeting)e.OldItems[0];
                this.appointmentsSource.Remove(removedAppointment);
                removedAppointment.PropertyChanged -= this.OnAppointmentPropertyChanged;
                break;
            default:
                break;
        }
    }

    private void OnAppointmentPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
    {
        if (e.PropertyName == nameof(OfficeMeeting.Location))
        {
            this.UpdateVisibleAppointments();
        }
    }

    private List<string> GetAvailableLocations()
    {
        var locations = new List<string>();

        if (this.isRoom501Checked)
        {
            locations.Add(DefaultLocations[0]);
        }

        if (this.isRoom502Checked)
        {
            locations.Add(DefaultLocations[1]);
        }

        if (this.isRoom503Checked)
        {
            locations.Add(DefaultLocations[2]);
        }

        return locations;
    }
}
