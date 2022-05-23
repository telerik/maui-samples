using SDKBrowserMaui;
using System.Collections.ObjectModel;
using System.Linq;
using System.Collections.Generic;

namespace SDKBrowser.Examples.ChartControl.PieChartCategory.PieChartPercentageExample
{
    public class ViewModel
    {
        public ObservableCollection<CategoricalData> Data { get; private set; }
        public double Total { get; private set; }

        public ViewModel()
        {
            this.Data = new ObservableCollection<CategoricalData>(DataProvider.GetCategoricalData());
            this.Total = this.Data.Sum(data => data.Value);
        }
    }
}
