using System.ComponentModel.DataAnnotations;

namespace TelerikCRM.Maui.Models.DataService;

public class Customer : ServiceModelBase<Customer>
{
    private string name;
    private string street;
    private string city;
    private string state;
    private string country;
    private string zipCode;
    private string notes;

    [Required]
    [Display(Name = "Name", Description = "Full name", Order = 0, Prompt = "Enter customer's full name")]
    [DataType(DataType.Text)]
    public string Name
    {
        get => this.name;
        set => this.SetProperty(ref this.name, value);
    }

    [Display(Name = "Street", Description = "Street", Order = 1, Prompt = "Enter street address")]
    [DataType(DataType.Text)]
    public string Street
    {
        get => this.street;
        set => this.SetProperty(ref this.street, value);
    }

    [Display(Name = "City", Description = "City", Order = 2, Prompt = "Enter city")]
    [DataType(DataType.Text)]
    public string City
    {
        get => this.city;
        set => this.SetProperty(ref this.city, value);
    }

    [Display(Name = "State", Description = "State", Order = 3, Prompt = "Enter state (2-letter)")]
    [DataType(DataType.Text)]
    public string State
    {
        get => this.state;
        set => this.SetProperty(ref this.state, value);
    }

    [Display(Name = "Country", Description = "Country", Order = 4, Prompt = "Enter country.")]
    [DataType(DataType.Text)]
    public string Country
    {
        get => this.country;
        set => this.SetProperty(ref this.country, value);
    }

    [Display(Name = "ZipCode", Description = "ZIP Code", Order = 5, Prompt = "Enter ZIP (5 or 4 digits)")]
    [DataType(DataType.Text)]
    public string ZipCode
    {
        get => this.zipCode;
        set => this.SetProperty(ref this.zipCode, value);
    }

    [Display(Name = "Notes", Description = "Notes", Order = 6, Prompt = "Enter any notes about this customer.")]
    [DataType(DataType.Text)]
    public string Notes
    {
        get => this.notes;
        set => this.SetProperty(ref this.notes, value);
    }

    public override bool Equals(Customer other)
        => other != null && other.Id == this.Id && other.Name == this.Name;

    public override Customer Copy()
    {
        var customer = new Customer();
        customer.CopyFrom(this);

        return customer;
    }

    public override void CopyFrom(Customer other)
    {
        this.Name = other.name;
        this.Street = other.street;
        this.City = other.city;
        this.State = other.state;
        this.Country = other.country;
        this.ZipCode = other.zipCode;
        this.Notes = other.notes;
    }
}