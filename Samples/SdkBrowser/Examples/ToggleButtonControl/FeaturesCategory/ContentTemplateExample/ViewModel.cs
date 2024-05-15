using System.Collections.ObjectModel;

namespace SDKBrowserMaui.Examples.ToggleButtonControl.FeaturesCategory.ContentTemplateExample;

// >> togglebutton-contenttemplate-viewmodel
public class ViewModel
{
    public ViewModel()
    {
        this.Mails = new ObservableCollection<Mail> 
        {
            new Mail{ Sender = "Terry Tye",  Subject = "Re: Summer Vacation" , IsImportant = true},
            new Mail{ Sender = "Felicia Keegan",  Subject = "Seminar Invitation", IsImportant = true},
            new Mail{ Sender = "Jared Linton",  Subject = "Discount code"},
            new Mail{ Sender = "Mark Therese",  Subject = "Quick feedback", IsImportant = true},
            new Mail{ Sender = "Elvina Randall",  Subject = "Happy Birthday!"},
            new Mail{ Sender = "Emilia Porter",  Subject = "Check the attachment", IsImportant = true},
            new Mail{ Sender = "Jared Linton",  Subject = "Gillian Flynn"},
            new Mail{ Sender = "Felicia Keegan",  Subject = "Re: Summer Vacation"},
            new Mail{ Sender = "Felicia Keegan",  Subject = "Pictures"},
        };
    }

    public ObservableCollection<Mail> Mails { get; set; }
}
// << togglebutton-contenttemplate-viewmodel
