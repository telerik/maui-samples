using SDKBrowserMaui.Examples.AutoCompleteControl.Models;
using System.Collections.ObjectModel;

namespace SDKBrowserMaui.Examples.AutoCompleteControl.ViewModels
{
    // >> autocomplete-extended-clients-viewmodel
    public class ExtendedClientsViewModel
    {
        public ExtendedClientsViewModel()
        {
            this.Source = new ObservableCollection<Client>()
            {
                new Client("Freda Curtis", "available.png"),
                new Client("Jeffery Francis", "away.png"),
                new Client("Eva Lawson", "available.png"),
                new Client("Emmett Santos", "away.png"),
                new Client("Theresa Bryan", "away.png"),
                new Client("Jenny Fuller", "available.png"),
                new Client("Terrell Norris", "away.png"),
                new Client("Eric Wheeler", "busy.png"),
                new Client("Julius Clayton", "available.png"),
                new Client("Alfredo Thornton", "busy.png"),
                new Client("Roberto Romero", "busy.png"),
                new Client("Orlando Mathis", "available.png"),
                new Client("Eduardo Thomas", "away.png"),
                new Client("Harry Douglas", "available.png"),
                new Client("Parker Blanton", "available.png"),
                new Client("Leanne Motton", "busy.png"),
                new Client("Shanti Osborn", "available.png"),
                new Client("Merry Lasker", "busy.png"),
                new Client("Jess Doyon", "away.png"),
                new Client("Kizzie Arjona", "busy.png"),
                new Client("Augusta Hentz", "available.png"),
                new Client("Tasha Trial", "away.png"),
                new Client("Fredda Boger", "busy.png"),
                new Client("Megan Mowery", "available.png"),
                new Client("Hong Telesco", "away.png"),
                new Client("Inez Landi", "busy.png"),
                new Client("Taina Cordray", "available.png"),
                new Client("Shantel Jarrell", "busy.png"),
                new Client("Soo Heidt", "available.png"),
                new Client("Rayford Mahon", "away.png"),
                new Client("Jenny Omarah", "away.png"),
                new Client("Denita Dalke", "available.png"),
                new Client("Nida Carty", "available.png"),
                new Client("Sharolyn Lambson", "away.png"),
                new Client("Niki Samaniego", "available.png"),
                new Client("Rudy Jankowski", "away.png"),
                new Client("Matha Whobrey", "busy.png"),
                new Client("Jessi Knouse", "available.png"),
                new Client("Vena Rieser", "away.png"),
                new Client("Roosevelt Boyce", "available.png"),
                new Client("Kristan Swiney", "away.png"),
                new Client("Lauretta Pozo", "busy.png"),
                new Client("Jarvis Victorine", "away.png"),
                new Client("Dane Gabor", "busy.png")
            };
        }
        public ObservableCollection<Client> Source { get; set; }
    }
    // << autocomplete-extended-clients-viewmodel
}
