using System;

namespace QSF.Examples.DataGridControl.SelectionExample;

public class Flight
{
    public DateTime DateTime { get; set; }

    public string Id { get; set; }

    public string DestinationFullName { get; set; }

    public string DestinationShortName { get; set; }

    public bool IsDirect { get; set; }

    public string AirlineName { get; set; }

    public string Status { get; set; }
}
