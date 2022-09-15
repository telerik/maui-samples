using System;
using System.ComponentModel.DataAnnotations;
using Telerik.Maui.Controls;

namespace QSF.Examples.DataFormControl.CustomizationExample;

public class AccountViewModel : NotifyPropertyChangedBase
{
    private string name;
    private string userName;
    private DateOnly birthDate;
    private GenderType gender;
    private string password;
    private string email;
    private string phone;

    public AccountViewModel()
    {
        this.BirthDate = DateOnly.FromDateTime(DateTime.Now);
    }

    [Required]
    [Display(Name = "Name", Prompt = "Enter Name")]
    public string Name
    {
        get => this.name;
        set => this.UpdateValue(ref this.name, value);
    }

    [Required]
    [Display(Name = "User Name", Prompt = "Enter User Name")]
    public string UserName
    {
        get => this.userName;
        set => this.UpdateValue(ref this.userName, value);
    }

    [Required]
    [Display(Name = "Birth Date", Prompt = "Select Date")]
    public DateOnly BirthDate
    {
        get => this.birthDate;
        set => this.UpdateValue(ref this.birthDate, value);
    }

    [Display(Name = "Gender")]
    public GenderType Gender
    {
        get => this.gender;
        set => this.UpdateValue(ref this.gender, value);
    }

    [Required]
    [Display(Name = "Password", Prompt = "Enter Password")]
    [StringLength(32, MinimumLength = 8)]
    public string Password
    {
        get => this.password;
        set => this.UpdateValue(ref this.password, value);
    }

    [Required]
    [EmailAddress]
    [Display(Name = "Email", Prompt = "Enter Email")]
    public string Email
    {
        get => this.email;
        set => this.UpdateValue(ref this.email, value);
    }

    [Required]
    [Phone]
    [Display(Name = "Phone", Prompt = "Enter Phone")]
    public string Phone
    {
        get => this.phone;
        set => this.UpdateValue(ref this.phone, value);
    }
}
