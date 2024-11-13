using Microsoft.Maui.Controls.Xaml;
using Telerik.Maui.Controls;
using System;
using System.ComponentModel.DataAnnotations;
using QSF.Examples.DataFormControl.FirstLookExample;

namespace QSF.Examples.DataFormControl.ThemingExample;

public class BookingViewModel : NotifyPropertyChangedBase
{
    private string firstName;
    private string lastName;
    private DateTime? startDate;
    private DateTime? endDate;
    private double? people = 1;
    private string phone;
    private EnumValue accommodation = EnumValue.Apartment;
    private ReservationType reservation;

    public enum EnumValue
    {
        Single,
        Double,
        Apartment,
    }

    [Required]
    [Display(Name = "First Name")]
    public string FirstName
    {
        get => this.firstName;
        set => this.UpdateValue(ref this.firstName, value);
    }

    [Required]
    [Display(Name = "Last Name")]
    public string LastName
    {
        get => this.lastName;
        set => this.UpdateValue(ref this.lastName, value);
    }

    [Required]
    [Phone]
    [Display(Name = "Phone", Prompt = "Enter Phone")]
    public string Phone
    {
        get => this.phone;
        set => this.UpdateValue(ref this.phone, value);
    }

    [Display(Name = "Start date")]
    public DateTime? StartDate
    {
        get => this.startDate;
        set => this.UpdateValue(ref this.startDate, value);
    }

    [Display(Name = "End Date")]
    public DateTime? EndDate
    {
        get => this.endDate;
        set => this.UpdateValue(ref this.endDate, value);
    }

    [Display(Name = "Guests")]
    public double? People
    {
        get => this.people;
        set => this.UpdateValue(ref this.people, value);
    }

    [Display(Name = "Rooms")]
    public EnumValue Accommodation
    {
        get => this.accommodation;
        set => this.UpdateValue(ref this.accommodation, value);
    }

    [Display(Name = "Reserved by")]
    public ReservationType Reservation
    {
        get => this.reservation;
        set => this.UpdateValue(ref this.reservation, value);
    }
}
