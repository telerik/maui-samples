using Microsoft.Maui.Graphics;
using QSF.ViewModels;
using System.Collections.ObjectModel;

namespace QSF.Examples.CollectionViewControl.DragVisualTemplateExample;

public class DragVisualTemplateViewModel : ExampleViewModel
{
    public DragVisualTemplateViewModel()
    {
        this.Destinations = new ObservableCollection<Destination>
        {
            new Destination() { Country = "Belgium", Name = "Brussels", Description = "Chocolate, beer, music and surrealism.", Icon = "\ue900" },
            new Destination() { Country = "Portugal", Name = "Porto", Description = "Taste it, drink it, eat it, love it. Bem-vindo ao Porto!", Icon = "\ue87f" },
            new Destination() { Country = "Spain", Name = "Malaga", Description = "Enjoy the perfect climat.", Icon = "\ue901" },
            new Destination() { Country = "Hungary", Name = "Budapest", Description = "One of the most exciting cities in the world.", Icon = "\ue87f" },
            new Destination() { Country = "Slovakia", Name = "Bratislava", Description = "A modern city on the Danube.", Icon = "\ue86a" },
            new Destination() { Country = "Italy", Name = "Florence", Description = "Love and culture are everywhere!", Icon = "\ue87f" },
            new Destination() { Country = "Poland", Name = "Poznan", Description = "A unique heritage with rich cultural offer.", Icon = "\ue86a" },
            new Destination() { Country = "Greece", Name = "Athens", Description = "The biggest open-air museum in Europe.", Icon = "\ue86a" },
            new Destination() { Country = "Bulgaria", Name = "Sofia", Description = "One of Europe`s oldest cities.", Icon = "\ue86a" },
            new Destination() { Country = "France", Name = "Bordeaux", Description = "Discover exciting new facets of its character.", Icon = "\ue900" },
            new Destination() { Country = "Switzerland", Name = "Geneva", Description = "One of the most welcoming cities in Europe.", Icon = "\ue87f" },
            new Destination() { Country = "Latvia", Name = "Riga", Description = "At the crossroads of various nations and cultures.", Icon = "\ue900" },
            new Destination() { Country = "Spain", Name = "Seville", Description = "Seville. Any time of year...", Icon = "\ue901" },
            new Destination() { Country = "France", Name = "Colmar", Description = "A condensed version of the Alsace region.", Icon = "\ue86a" },
            new Destination() { Country = "Austria", Name = "Vienna", Description = "The Giant Ferris Wheel is awaiting you.", Icon = "\ue900" },
            new Destination() { Country = "France", Name = "Montpellier", Description = "Smart, Mediterranean, attractive, welcoming and festive.", Icon = "\ue87f" },
            new Destination() { Country = "Spain", Name = "Valencia", Description = "Sun, culture, history and future. ", Icon = "\ue901" },
            new Destination() { Country = "Spain", Name = "Barcelona", Description = "Barcelona never sleeps.", Icon = "\ue900" },
            new Destination() { Country = "Italy", Name = "Milan", Description = "The hub of Italian culture", Icon = "\ue900" },
            new Destination() { Country = "Poland", Name = "Gdansk", Description = "You`ll be amazed by the beauty of Gdansk.", Icon = "\ue87f" },
            new Destination() { Country = "Italy", Name = "Rome", Description = "Treat yourself to a stay in the Eternal City.", Icon = "\ue901" },
            new Destination() { Country = "Scotland", Name = "Edinburgh", Description = "Shopping, dining & architectural splendour.", Icon = "\ue86a" },
            new Destination() { Country = "Portugal", Name = "Lisbon", Description = "The pure pleasure of being in one of the best cities in the world.", Icon = "\ue901" }
        };
    }

    public ObservableCollection<Destination> Destinations { get; set; }
}
