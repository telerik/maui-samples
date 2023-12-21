using System;
using System.Collections.Generic;
using Telerik.Maui.Controls;

namespace SDKBrowserMaui.Examples.DataGridControl.ColumnsCategory;

// >> datagrid-club-model
public class Club : NotifyPropertyChangedBase
{
    private string name;
    private DateTime established;
    private int stadiumCapacity;
    private bool isChampion;
    private string country;
    private string city;
    private string championship;
    private double? revenue;

    public Club(string name, DateTime established, int stadiumCapacity, string country, string city, string championship, double? revenue)
    {
        Name = name;
        Established = established;  
        StadiumCapacity = stadiumCapacity;
        Country = country;
        City = city;
        Championship = championship;
        Revenue = revenue;
    }

    public string Name
    {
        get { return this.name; }
        set { this.UpdateValue(ref this.name, value); }
    }
    public DateTime Established
    {
        get { return this.established; }
        set { this.UpdateValue(ref this.established, value); }
    }

    public int StadiumCapacity
    {
        get { return this.stadiumCapacity; }
        set { this.UpdateValue(ref this.stadiumCapacity, value); }
    }

    public bool IsChampion
    {
        get { return this.isChampion; }
        set { this.UpdateValue(ref this.isChampion, value); }
    }

    public string Country
    {
        get { return this.country; }
        set { this.UpdateValue(ref this.country, value); }
    }

    public string City
    {
        get { return this.city; }
        set { this.UpdateValue(ref this.city, value); }
    }

    public string Championship       
    {
        get { return this.championship; }
        set { this.UpdateValue(ref this.championship, value); }
    }

    public double? Revenue
    {
        get { return this.revenue; }
        set { this.UpdateValue(ref this.revenue, value); }
    }

    public List<string> Championships => new List<string> { "UEFA Champions League", "Premier League", "La Liga" };
}
// << datagrid-club-model