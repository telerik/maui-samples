using System;

namespace QSF.Examples.ChartControl;

public class FinancialDataItem
{
    public string Date { get; set; }
    public double Open { get; set; }
    public double High { get; set; }
    public double Low { get; set; }
    public double Close { get; set; }

    public DateTime DateCategory
    {
        get
        {
            return DateTime.ParseExact(this.Date, "dd-MM-yyyy", null);
        }
    }
}