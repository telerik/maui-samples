using System.ComponentModel.DataAnnotations;
using TelerikCRM.Maui.Services;

namespace TelerikCRM.Maui.Models.DataService;

public class Employee : ServiceModelBase<Employee>
{
    private string name;
    private string photoUri = "profile_photo.png";
    private string officeLocation;
    private DateTime hireDate = DateTime.Today;
    private double salary;
    private int vacationBalance;
    private int vacationUsed;
    private string notes;

    [Required]
    [Display(Name = "Name", Description = "employee name", GroupName = "Personal", Order = 0, Prompt = "Enter full name")]
    [DataType(DataType.Text)]
    public string Name
    {
        get => this.name;
        set => this.SetProperty(ref this.name, value);
    }

    public string PhotoUri
    {
        get => this.photoUri;
        set
        {
            if (this.SetProperty(ref this.photoUri, value))
            {
                ImageCache.Invalidate(value);
                this.OnPropertyChanged(nameof(this.PhotoImageSource));
            }
        }
    }

    public ImageSource PhotoImageSource => ImageCache.GetImageSource(this.photoUri) ?? ImageSource.FromFile("profile_photo.png");

    [Required]
    [Display(Name = "Office Location", Description = "Home office", GroupName = "Personal", Order = 1, Prompt = "Enter home office location")]
    [DataType(DataType.Text)]
    public string OfficeLocation
    {
        get => this.officeLocation;
        set => this.SetProperty(ref this.officeLocation, value);
    }

    [Required]
    [Display(Name = "Hire Date", GroupName = "Personal", Order = 2)]
    [DataType(DataType.Date)]
    public DateTime HireDate
    {
        get => this.hireDate;
        set => this.SetProperty(ref this.hireDate, value);
    }

    [Required]
    [Display(Name = "Salary", GroupName = "Personal", Order = 3)]
    [DataType(DataType.Currency)]
    [DisplayFormat(DataFormatString = "C0")]
    [Range(0, 10000000)]
    public double Salary
    {
        get => this.salary;
        set => this.SetProperty(ref this.salary, value);
    }

    [Required]
    [Display(Name = "Vacation Total", GroupName = "PTO", Order = 0)]
    [DisplayFormat(DataFormatString = "C1")]
    [Range(0, 360)]
    public int VacationBalance
    {
        get => this.vacationBalance;
        set => this.SetProperty(ref this.vacationBalance, value);
    }

    [Display(Name = "Vacation Used", GroupName = "PTO", Order = 1)]
    [DisplayFormat(DataFormatString = "C1")]
    [Range(0, 360)]
    public int VacationUsed
    {
        get => this.vacationUsed;
        set => this.SetProperty(ref this.vacationUsed, value);
    }

    [Display(Name = "Notes", Description = "employee notes", GroupName = "Notes", Order = 0, Prompt = "Enter special notes")]
    [DataType(DataType.MultilineText)]
    public string Notes
    {
        get => this.notes;
        set => this.SetProperty(ref this.notes, value);
    }

    public override bool Equals(Employee other)
        => other != null && other.Id == this.Id && other.Name == this.Name;

    public override Employee Copy()
    {
        var employee = new Employee();
        employee.CopyFrom(this);

        return employee;
    }

    public override void CopyFrom(Employee other)
    {
        this.Name = other.name;
        this.PhotoUri = other.photoUri;
        this.OfficeLocation = other.officeLocation;
        this.HireDate = other.hireDate;
        this.Salary = other.salary;
        this.VacationBalance = other.vacationBalance;
        this.VacationUsed = other.vacationUsed;
        this.Notes = other.notes;
    }
}