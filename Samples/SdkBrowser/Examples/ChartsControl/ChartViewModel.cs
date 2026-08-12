using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace SDKBrowserMaui.Examples.ChartsControl;

// >> chart-categorical-viewmodel
public class CategoricalViewModel
{
    public ObservableCollection<CategoricalData> Data { get; } = new ObservableCollection<CategoricalData>
    {
        new CategoricalData { Category = "Jan", Value = 42 },
        new CategoricalData { Category = "Feb", Value = 58 },
        new CategoricalData { Category = "Mar", Value = 37 },
        new CategoricalData { Category = "Apr", Value = 71 },
        new CategoricalData { Category = "May", Value = 64 },
        new CategoricalData { Category = "Jun", Value = 88 },
    };
}
// << chart-categorical-viewmodel

// >> chart-multiseries-viewmodel
public class MultiSeriesViewModel
{
    public ObservableCollection<MultiSeriesData> Data { get; } = new ObservableCollection<MultiSeriesData>
    {
        new MultiSeriesData { Category = "Q1", ProductA = 45, ProductB = 37 },
        new MultiSeriesData { Category = "Q2", ProductA = 58, ProductB = 47 },
        new MultiSeriesData { Category = "Q3", ProductA = 37, ProductB = 62 },
        new MultiSeriesData { Category = "Q4", ProductA = 71, ProductB = 55 },
    };
}
// << chart-multiseries-viewmodel

// >> chart-datetime-viewmodel
public class DateTimeViewModel
{
    public ObservableCollection<DateTimeData> Data { get; } = new ObservableCollection<DateTimeData>
    {
        new DateTimeData { Date = new DateTime(2024, 1, 1), Value = 42 },
        new DateTimeData { Date = new DateTime(2024, 2, 1), Value = 58 },
        new DateTimeData { Date = new DateTime(2024, 3, 1), Value = 37 },
        new DateTimeData { Date = new DateTime(2024, 4, 1), Value = 71 },
        new DateTimeData { Date = new DateTime(2024, 5, 1), Value = 64 },
        new DateTimeData { Date = new DateTime(2024, 6, 1), Value = 88 },
    };
}
// << chart-datetime-viewmodel

// >> chart-pie-viewmodel
public class PieViewModel
{
    public ObservableCollection<PieData> Data { get; } = new ObservableCollection<PieData>
    {
        new PieData { Label = "Mobile", Value = 45 },
        new PieData { Label = "Desktop", Value = 30 },
        new PieData { Label = "Tablet", Value = 15 },
        new PieData { Label = "Other", Value = 10 },
    };
}
// << chart-pie-viewmodel

// >> chart-pointseries-viewmodel
public class PointSeriesViewModel
{
    public ObservableCollection<PointData> Data { get; } = new ObservableCollection<PointData>
    {
        new PointData { XValue = 5, YValue = 12 },
        new PointData { XValue = 18, YValue = 42 },
        new PointData { XValue = 27, YValue = 30 },
        new PointData { XValue = 39, YValue = 71 },
        new PointData { XValue = 52, YValue = 55 },
        new PointData { XValue = 64, YValue = 88 },
        new PointData { XValue = 78, YValue = 47 },
        new PointData { XValue = 91, YValue = 63 },
    };
}
// << chart-pointseries-viewmodel
