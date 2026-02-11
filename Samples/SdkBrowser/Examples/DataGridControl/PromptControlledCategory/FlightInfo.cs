using System;

namespace SDKBrowserMaui.Examples.DataGridControl.PromptControlledCategory;

// >> datagrid-prompt-model
public class FlightInfo
{
    public string Company { get; set; }

    public int FlightNumber { get; set; }

    public TimeSpan ArrivalTime { get; set; }

    public TimeSpan DepartureTime { get; set; }

    public string From { get; set; }

    public string To { get; set; }
}
// << datagrid-prompt-model