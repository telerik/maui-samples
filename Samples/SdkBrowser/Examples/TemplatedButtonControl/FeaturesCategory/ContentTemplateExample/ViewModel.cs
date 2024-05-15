using System.Collections.ObjectModel;

namespace SDKBrowserMaui.Examples.TemplatedButtonControl.FeaturesCategory.ContentTemplateExample;

// >> templatedbutton-contenttemplate-viewmodel
public class ViewModel
{
    public ViewModel()
    {
        this.Contacts = new ObservableCollection<Contact>
        {
            new Contact() { Name = "Ambulance" },
            new Contact() { Name = "Brian Johnas" },
            new Contact() { Name = "Dad", IsFavorite = true },
            new Contact() { Name = "Ema Lawson" },
            new Contact() { Name = "Emergency" },
            new Contact() { Name = "Emmett Fuller" },
            new Contact() { Name = "Eric Wheeler" },
            new Contact() { Name = "Fire Brigade" },
            new Contact() { Name = "Freda Curtis" },
            new Contact() { Name = "Jeffery Francis" },
            new Contact() { Name = "Jenny Santos" },
            new Contact() { Name = "Mom", IsFavorite = true },
            new Contact() { Name = "Niki Samaniego" },
            new Contact() { Name = "Police" },
            new Contact() { Name = "Voice Mail" }
        };
    }

    public ObservableCollection<Contact> Contacts { get; set; }
}
// << templatedbutton-contenttemplate-viewmodel
