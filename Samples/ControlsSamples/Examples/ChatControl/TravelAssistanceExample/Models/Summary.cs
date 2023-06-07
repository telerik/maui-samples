using System.Collections.Generic;

namespace QSF.Examples.ChatControl.TravelAssistanceExample.Models;

public class Summary
{
    public string Hotel { get; set; }
    public List<string> Flights { get; set; }
    public string TotalPrice { get; set; }
    public string Title { get; set; }
    public string Image { get; set; }
}