using System;
using System.Collections.ObjectModel;

namespace SDKBrowserMaui.Examples.ChartsControl;

// >> chart-datamodel-categorical-data
public class CategoricalData
{
    public string Category { get; set; }

    public double Value { get; set; }
}
// << chart-datamodel-categorical-data

// >> chart-datamodel-multiseriesdata
public class MultiSeriesData
{
    public string Category { get; set; }

    public double ProductA { get; set; }

    public double ProductB { get; set; }
}
// << chart-datamodel-multiseriesdata

// >> chart-datamodel-datetime
public class DateTimeData
{
    public DateTime Date { get; set; }

    public double Value { get; set; }
}
// << chart-datamodel-datetime

// >> chart-datamodel-piedata
public class PieData
{
    public string Label { get; set; }

    public double Value { get; set; }
}
// << chart-datamodel-piedata

// >> chart-datamodel-pointdata
public class PointData
{
    public double XValue { get; set; }

    public double YValue { get; set; }
}
// << chart-datamodel-pointdata
