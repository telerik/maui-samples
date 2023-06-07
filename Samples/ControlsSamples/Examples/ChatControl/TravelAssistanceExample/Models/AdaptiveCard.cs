using System.Collections.Generic;

namespace QSF.Examples.ChatControl.TravelAssistanceExample.Models;

public class AdaptiveCard
{
    public List<AdaptiveElement> Body { get; set; }
    public string TType { get; set; }
}