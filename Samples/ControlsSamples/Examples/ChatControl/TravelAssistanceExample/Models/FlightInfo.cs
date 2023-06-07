using System;
using Telerik.Maui.Controls;

namespace QSF.Examples.ChatControl.TravelAssistanceExample.Models;

public class FlightInfo : NotifyPropertyChangedBase
{
    private string departureCity;
    private string arrivalCity;
    private string departureAirport;
    private string arrivalAirport;
    private DateTime departureDate;
    private DateTime arrivalDate;
    private string planeImageUrl;

    public string DepartureCity
    {
        get
        {
            return this.departureCity;
        }
        set
        {
            this.UpdateValue(ref this.departureCity, value);
        }
    }

    public string ArrivalCity
    {
        get
        {
            return this.arrivalCity;
        }
        set
        {
            this.UpdateValue(ref this.arrivalCity, value);
        }
    }

    public string DepartureAirport
    {
        get
        {
            return this.departureAirport;
        }
        set
        {
            this.UpdateValue(ref this.departureAirport, value);
        }
    }

    public string ArrivalAirport
    {
        get
        {
            return this.arrivalAirport;
        }
        set
        {
            this.UpdateValue(ref this.arrivalAirport, value);
        }
    }

    public DateTime DepartureDate
    {
        get
        {
            return this.departureDate;
        }
        set
        {
            this.UpdateValue(ref this.departureDate, value);
        }
    }

    public DateTime ArrivalDate
    {
        get
        {
            return this.arrivalDate;
        }
        set
        {
            this.UpdateValue(ref this.arrivalDate, value);
        }
    }

    public string PlaneImageUrl
    {
        get
        {
            return this.planeImageUrl;
        }
        set
        {
            this.UpdateValue(ref this.planeImageUrl, value);
        }
    }
}