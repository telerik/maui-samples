using System;

namespace QSF.Examples.DataGridControl.ColumnTypesExample
{
    public class Flight
    {
        public DateTime Date { get; set; }

        public DateTime Time { get; set; }

        public string Id { get; set; }

        public int Terminal { get; set; }

        public string DestinationFullName { get; set; }

        public string DestinationShortName { get; set; }

        public bool IsDirect { get; set; }

        public string AirlineName { get; set; }

        public string Status { get; set; }
        public string StatusTime { get; set; }
    }
}
