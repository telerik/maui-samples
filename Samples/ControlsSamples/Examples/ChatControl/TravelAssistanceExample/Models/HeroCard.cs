using System.Collections.Generic;

namespace QSF.Examples.ChatControl.TravelAssistanceExample.Models;

public class HeroCard
{
    public string Title { get; set; }
    public string Subtitle { get; set; }
    public string Text { get; set; }
    public IList<CardImage> Images { get; set; }
    public IList<CardAction> Buttons { get; set; }
    public CardAction Tap { get; set; }
}