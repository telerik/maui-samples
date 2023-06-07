using System.Collections.Generic;

namespace QSF.Examples.ChatControl.TravelAssistanceExample.Models;

public class ActivityRoot
{
    public List<Activity> Activities { get; set; }
    public string Watermark { get; set; }
}