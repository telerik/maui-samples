using SDKBrowserMaui.Data;
using System;
using System.Collections.ObjectModel;

namespace SDKBrowserMaui.Examples.ChartsControl.SeriesCategory;

// >> chart-financial-series-viewmodel
public class FinancialViewModel
{
    public FinancialViewModel()
    {
        this.Data = new ObservableCollection<OhlcDataPoint>
        {
            new OhlcDataPoint { Category = new DateTime(2024, 1, 1), Open = 5, High = 10, Low = 2, Close = 8 },
            new OhlcDataPoint { Category = new DateTime(2024, 2, 1), Open = 7, High = 15, Low = 3, Close = 5 },
            new OhlcDataPoint { Category = new DateTime(2024, 3, 1), Open = 15, High = 20, Low = 10, Close = 19 },
            new OhlcDataPoint { Category = new DateTime(2024, 4, 1), Open = 2, High = 7, Low = 1, Close = 5 },
            new OhlcDataPoint { Category = new DateTime(2024, 5, 1), Open = 15, High = 25, Low = 10, Close = 12 },
            new OhlcDataPoint { Category = new DateTime(2024, 6, 1), Open = 12, High = 18, Low = 9, Close = 16 },
            new OhlcDataPoint { Category = new DateTime(2024, 7, 1), Open = 16, High = 22, Low = 11, Close = 13 },
        };
    }

    public ObservableCollection<OhlcDataPoint> Data { get; }
}
// << chart-financial-series-viewmodel
