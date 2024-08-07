using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace QSF.Examples.CollectionViewControl.ConfigurationExample;

public static class ContactsProvider
{
    public static ObservableCollection<Contact> GetContacts()
    {
        return new ObservableCollection<Contact>()
        {
            new Contact { Name = "Eva Lawson" },
            new Contact { Name = "Layton Buck" },
            new Contact { Name = "Chester Harvey", IsFavorite = true },
            new Contact { Name = "Jenny Fuller" },
            new Contact { Name = "Ashley Robertson" },
            new Contact { Name = "Niki Samaniego" },
            new Contact { Name = "Armstrong Robbie" },
            new Contact { Name = "Coby Ryan", IsFavorite = true },
            new Contact { Name = "Davis Anderson" },
            new Contact { Name = "Quincy Sanchez" },
            new Contact { Name = "Konny Mills" },
            new Contact { Name = "Casper Fletcher" },
            new Contact { Name = "Barnes Ashton", IsFavorite = true },
            new Contact { Name = "Bob Carty" },
            new Contact { Name = "Angus Barnes" },
            new Contact { Name = "Vada Davies" },
            new Contact { Name = "David Webb" },
            new Contact { Name = "Nida Carty", IsFavorite = true },
            new Contact { Name = "Jeffery Francis" },
            new Contact { Name = "Terrell Norris" },
            new Contact { Name = "Greg Nikolas" },
            new Contact { Name = "Barnaby Hunter" },
            new Contact { Name = "Peter Bence" },
            new Contact { Name = "Howard Pittman" },
            new Contact { Name = "Joel Walsh" },
            new Contact { Name = "Matias Santos" },
            new Contact { Name = "Xavi Vasquez", IsFavorite = true },
        };
    }
}