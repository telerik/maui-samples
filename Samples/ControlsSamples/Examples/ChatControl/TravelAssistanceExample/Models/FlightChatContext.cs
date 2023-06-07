using System.Collections.Generic;
using Telerik.Maui.Controls;

namespace QSF.Examples.ChatControl.TravelAssistanceExample.Models;

public class FlightChatContext : NotifyPropertyChangedBase
{
    private string passangerName;
    private string totalTicketPrice;

    public string PassangerName
    {
        get
        {
            return this.passangerName;
        }
        set
        {
            this.UpdateValue(ref this.passangerName, value);
        }
    }

    public string TotalTicketPrice
    {
        get
        {
            return this.totalTicketPrice;
        }
        set
        {
            this.UpdateValue(ref this.totalTicketPrice, value);
        }
    }

    public IList<FlightInfo> Flights { get; set; }
}