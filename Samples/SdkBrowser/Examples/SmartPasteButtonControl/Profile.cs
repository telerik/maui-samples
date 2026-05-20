using System;
using System.ComponentModel.DataAnnotations;
using Telerik.Maui.Controls;

namespace SDKBrowserMaui.Examples.SmartPasteButtonControl;

// >> smartpaste-viewmodel-dataform
public class Profile : NotifyPropertyChangedBase
{
    private string name;
    private string email;
    private string address;
    private string jobTitle;
    private string notes;

    [Required]
    [Display(Name = "Full Name")]
    public string Name
    {
        get => this.name;
        set => UpdateValue(ref this.name, value);
    }

    [Required]
    [EmailAddress]
    [Display(Name = "Email")]
    public string Email
    {
        get => this.email;
        set => UpdateValue(ref this.email, value);
    }

    [Display(Name = "Job Title")]
    public string JobTitle
    {
        get => this.jobTitle;
        set => UpdateValue(ref this.jobTitle, value);
    }

    [Display(Name="Address")]
    public string Address
    {
        get => this.address;
        set => UpdateValue(ref this.address, value);
    }

    [Display(Name = "Notes")]
    public string Notes
    {
        get => this.notes;
        set => UpdateValue(ref this.notes, value);
    }
}
// << smartpaste-viewmodel-dataform