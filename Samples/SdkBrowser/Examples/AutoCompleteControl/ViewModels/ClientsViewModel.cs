using SDKBrowserMaui.Examples.AutoCompleteControl.Models;
using System.Collections.ObjectModel;

namespace SDKBrowserMaui.Examples.AutoCompleteControl.ViewModels
{
    // >> autocomplete-clients-viewmodel
    public class ClientsViewModel
    {
        public ClientsViewModel()
        {
            this.Source = new ObservableCollection<Client>()
            {
                new Client("Freda Curtis", "fcurtis@mail.com", "available.png"),
                new Client("Jeffery Francis", "jfrancis@mail.com", "away.png"),
                new Client("Eva Lawson", "elawson@mail.com", "available.png"),
                new Client("Emmett Santos", "esantos@mail.com", "busy.png"),
                new Client("Theresa Bryan", "tbryan@mail.com", "available.png"),
                new Client("Jenny Fuller", "jfuller@mail.com", "busy.png"),
                new Client("Terrell Norris", "tnorris@mail.com", "away.png"),
                new Client("Eric Wheeler", "ewheeler@mail.com", "away.png"),
                new Client("Nida Carty", "ncarty@mail.com", "away.png"),
                new Client("Niki Samaniego", "nsamaniego@mail.com", "busy.png")
            };
        }

        public ObservableCollection<Client> Source { get; set; }
    }
    // << autocomplete-clients-viewmodel
}
