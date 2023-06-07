using System.Collections.Generic;
using System.Windows.Input;
using Microsoft.Maui.Controls;
using QSF.Services;
using QSF.ViewModels;
using Telerik.Maui.Controls.RichTextEditor;

namespace QSF.Examples.RichTextEditorControl.FirstLookExample;

public class FirstLookViewModel : ExampleViewModel
{
    private readonly IResourceService resourceService;

    public FirstLookViewModel()
    {
        this.resourceService = DependencyService.Get<IResourceService>();

        this.RecipientsItemSource = new List<string>
        {
            "VacationWander",
            "Joshua Price",
            "Reuben Holmes", 
            "Eva Lawson",
            "Rory Baxter", 
            "David Webb", 
            "Howard Pittman",
            "Davis Anderson",
            "Cannon Puckett",
            "Xavi Vasquez",
            "Ronald Hatfield",
            "Freda Curtis",
            "Jeffery Francis",
            "Eva Lawson",
            "Emmett Santos", 
            "Vada Davies",
            "Jenny Fuller",
            "Terrell Norris",
            "Vadim Saunders",
            "Nida Carty",
            "Niki Samaniego",
            "Valdex Mills",
            "Layton Buck",
            "Alex Ramos",
            "Alena Cline",
            "Joel Walsh", 
            "Vadik Pearson", 
            "Bob Carty",
            "Carol Samaniego",
            "Greg Nikolas",
            "Ivan Ivanov",
            "Konny Mills",
            "Matias Santos", 
            "Peter Bence",
            "Quincy Sanchez",
        };

        this.Source = RichTextSource.FromStream(() => this.resourceService.GetResourceStream("PickYourHoliday.html"));
        this.SendEmailCommand = new Command(SendEmail);
    }

    public RichTextSource Source { get; set; }

    public List<string> RecipientsItemSource { get; set; }

    public ICommand SendEmailCommand { get; set; }

    private void SendEmail()
    {
        var toastMessage = "Email sent.";
        var toastService = DependencyService.Get<IToastMessageService>();

        toastService.ShortAlert(toastMessage);
    }
}
