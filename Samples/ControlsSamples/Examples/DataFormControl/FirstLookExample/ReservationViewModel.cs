using System;
using System.ComponentModel.DataAnnotations;
using Telerik.Maui.Controls;

namespace QSF.Examples.DataFormControl.FirstLookExample;

public class ReservationViewModel : NotifyPropertyChangedBase
{
    private string name;
    private string phone;
    private DateOnly date;
    private TimeOnly time;
    private int guests;
    private SectionType section;
    private ReservationType reservation;

    public ReservationViewModel()
    {
        var currentTime = DateTime.Now;

        this.Date = DateOnly.FromDateTime(currentTime);
        this.Time = TimeOnly.FromDateTime(currentTime);
        this.Guests = 1;
    }

    [Required]
    [Display(Name = "Name", Prompt = "Enter Name")]
    public string Name
    {
        get => this.name;
        set => this.UpdateValue(ref this.name, value);
    }

    [Required]
    [Phone]
    [Display(Name = "Phone", Prompt = "Enter Phone")]
    public string Phone
    {
        get => this.phone;
        set => this.UpdateValue(ref this.phone, value);
    }

    [Display(Name = "Date", Prompt = "Select Date")]
    public DateOnly Date
    {
        get => this.date;
        set => this.UpdateValue(ref this.date, value);
    }

    [Display(Name = "Time", Prompt = "Select Time")]
    public TimeOnly Time
    {
        get => this.time;
        set => this.UpdateValue(ref this.time, value);
    }

    [Range(1, 20)]
    [Display(Name = "Guests")]
    public int Guests
    {
        get => this.guests;
        set => this.UpdateValue(ref this.guests, value);
    }

    [Display(Name = "Section")]
    public SectionType Section
    {
        get => this.section;
        set => this.UpdateValue(ref this.section, value);
    }

    [Display(Name = "Reserved by")]
    public ReservationType Reservation
    {
        get => this.reservation;
        set => this.UpdateValue(ref this.reservation, value);
    }
}
