using CryptoTracker.Data;
using Microsoft.Maui.Controls;
using System;
using System.Collections.Generic;
using Telerik.Maui.Controls;
using Telerik.Maui.Controls.Compatibility.Chart;

namespace CryptoTracker.ViewModels
{
    public class CoinInfoViewModel : NotifyPropertyChangedBase
    {
        private const int LineChartType = 0;
        private const int CandlestickChartType = 1;

        private double coinCurrentPrice;
        private string coinName;
        private string coinSymbol;
        private DateTime fromDate;
        private DateTime toDate;
        private int selectedTimePeriod;
        private double coinPriceChangePercentage;
        private TimeInterval chartMajorStepUnit;
        private double chartMajorStep;
        private IList<CoinData> dataForChart;
        private IList<CoinData> dataForDataGrid;
        private IList<string> timePeriods;
        private bool isLineChartVisible;
        private bool isCandlestickChartVisible;
        private int selectedChartType;
        private string chartLabelFormat;

        public CoinInfoViewModel()
        {
            this.ChartMajorStep = 1.0;
            this.DataForChart = new List<CoinData>();
            this.DataForDataGrid = new List<CoinData>();
            this.TimePeriods = new List<string>() { "1D", "1W", "1M", "6M", "1Y"};
            this.IsLineChartVisible = true;
        }

        public IList<CoinData> DataForChart
        {
            get => this.dataForChart;
            private set => this.UpdateValue(ref this.dataForChart, value);
        }

        public IList<CoinData> DataForDataGrid
        {
            get => this.dataForDataGrid;
            private set => this.UpdateValue(ref this.dataForDataGrid, value);
        }

        public bool IsLineChartVisible
        {
            get => this.isLineChartVisible;
            private set => this.UpdateValue(ref this.isLineChartVisible, value);
        }

        public bool IsCandlestickChartVisible
        {
            get => this.isCandlestickChartVisible;
            private set => this.UpdateValue(ref this.isCandlestickChartVisible, value);
        }

        public IList<string> TimePeriods
        {
            get => this.timePeriods;
            private set => this.UpdateValue(ref this.timePeriods, value);
        }

        public int SelectedTimePeriod
        {
            get => this.selectedTimePeriod;
            set 
            {
                if (this.UpdateValue(ref this.selectedTimePeriod, value))
                {
                    this.OnDatePeriodSelection();
                }
            }
        }

        public int SelectedChartType
        {
            get => this.selectedChartType;
            set 
            {
                if (this.UpdateValue(ref this.selectedChartType, value))
                {
                    this.OnChartTypeSelection();
                }
            }
        }

        public double CoinCurrentPrice
        {
            get => this.coinCurrentPrice;
            private set => this.UpdateValue(ref this.coinCurrentPrice, Math.Round(value, 2));
        }

        public double CoinPriceChangePercentage
        {
            get => this.coinPriceChangePercentage;
            private set => this.UpdateValue(ref this.coinPriceChangePercentage, value);
        }

        public string CoinSymbol
        {
            get => this.coinSymbol;
            private set => this.UpdateValue(ref this.coinSymbol, value);
        }

        public string CoinName
        {
            get => this.coinName;
            private set => this.UpdateValue(ref this.coinName, value);
        }

        public DateTime FromDate
        {
            get => this.fromDate;
            private set => this.UpdateValue(ref this.fromDate, value);
        }

        public DateTime ToDate
        {
            get => this.toDate;
            private set => this.UpdateValue(ref this.toDate, value);
        }

        public TimeInterval ChartMajorStepUnit
        {
            get => this.chartMajorStepUnit;
            private set => this.UpdateValue(ref this.chartMajorStepUnit, value);
        }

        public double ChartMajorStep
        {
            get => this.chartMajorStep;
            private set => this.UpdateValue(ref this.chartMajorStep, value);
        }

        public string ChartLabelFormat
        {
            get => this.chartLabelFormat;
            private set => this.UpdateValue(ref this.chartLabelFormat, value);
        }

        public void InitializeCoinData(CoinData currentCoinInfo)
        {
            this.CoinName = currentCoinInfo.Name;
            this.CoinSymbol = currentCoinInfo.Symbol;
            this.CoinCurrentPrice = currentCoinInfo.ClosingPrice;
            this.CoinPriceChangePercentage = currentCoinInfo.ChangeInPricePercentage;
            this.ToDate = currentCoinInfo.Date;
            this.SelectedTimePeriod = 0; // so that UpdateValue gets called when we change from 2 to 2 (that happens when we select a new coin from the ListView)
            this.SelectedTimePeriod = 2;
            this.SelectedChartType = 1;
            this.SelectedChartType = 0; // so that UpdateValue gets called when we change from 0 to 0 (that happens when we select a new coin from the ListView)
        }

        private void LoadCoinData()
        {
            var coinService = DependencyService.Get<ICoinDataService>();
            var coinData = coinService.GetCoinDataFromDateToDate(this.CoinName, this.FromDate, this.ToDate);

            this.DataForChart = coinData;
            this.DataForDataGrid = coinData;

            if (this.TimePeriods[this.SelectedTimePeriod] == "1D")
            {
                this.DataForChart = this.Get24HourCoinInfo(this.DataForChart[0]);
            }
            else if (this.IsCandlestickChartVisible && this.TimePeriods[this.SelectedTimePeriod] != "1W" && this.DataForChart.Count > 0)
            {
                this.DataForChart = this.GetCandlestickChartData(this.DataForChart);
            }
        }

        private IList<CoinData> GetCandlestickChartData(IList<CoinData> initialData)
        {
            var newData = new List<CoinData>();

            var newElementAdditionInterval = this.TimePeriods[this.SelectedTimePeriod] == "1M" ? 7 : 30;
            
            var newOpen = 0.0;
            var newClose = 0.0;
            var newHigh = 0.0;
            var newLow = double.MaxValue;
            var newDate = DateTime.Now;
            bool isNewElement = true;

            for (int i = 0; i < initialData.Count; i++)
            {
                if (isNewElement)
                {
                    newOpen = initialData[i].OpeningPrice;
                    newDate = initialData[i].Date;

                    isNewElement = false;
                }

                newHigh = initialData[i].Price24High > newHigh ? initialData[i].Price24High : newHigh;
                newLow = initialData[i].Price24Low < newLow ? initialData[i].Price24Low : newLow;

                if (i % newElementAdditionInterval == 0 && i != 0)
                {
                    newClose = initialData[i].ClosingPrice;
                    newData.Add(new CoinData()
                    {
                        Name = initialData[i].Name,
                        Symbol = initialData[i].Symbol,
                        OpeningPrice = newOpen,
                        ClosingPrice = newClose,
                        Price24Low = newLow,
                        Price24High = newHigh,
                        Date = newDate,
                    });

                    newHigh = 0;
                    newLow = double.MaxValue;
                    isNewElement = true;
                }
            }

            return newData;
        }

        private IList<CoinData> Get24HourCoinInfo(CoinData current)
        {
            var data = new List<CoinData>();

            var priceToAdd = current.OpeningPrice;

            for (int i = 23; i >= 1; i--)
            {
                data.Add(new CoinData()
                {
                    Name = current.Name,
                    Date = current.Date.AddHours(-i).AddSeconds(1),
                    ClosingPrice = priceToAdd,
                });

                switch (i)
                {
                    case 10:
                        priceToAdd = current.Price24Low;
                        break;
                    case 20:
                        priceToAdd = current.Price24High;
                        break;
                    default:
                        priceToAdd = priceToAdd >= current.ClosingPrice
                            ? priceToAdd - (current.Price24High - current.Price24Low) / 23
                            : priceToAdd + (current.Price24High - current.Price24Low) / 23; ;
                        break;
                }
            }

            data.Add(current);

            return data;
        }

        private void OnDatePeriodSelection()
        {
            var timePeriod = this.TimePeriods[this.SelectedTimePeriod];

            if (this.ToDate.Year < 2000) return;
            this.ChartMajorStep = 1;

            switch (timePeriod)
            {
                case "1D":
                    this.FromDate = this.ToDate.AddDays(-1);
                    this.ChartMajorStepUnit = TimeInterval.Hour;
                    this.ChartLabelFormat = "h tt";
                    this.ChartMajorStep = 4;
                    break;
                case "1W":
                    this.FromDate = this.ToDate.AddDays(-7);
                    this.ChartMajorStepUnit = TimeInterval.Day;
                    this.ChartLabelFormat = "ddd";
                    break;
                case "1M":
                    this.FromDate = this.ToDate.AddMonths(-1);
                    this.ChartMajorStepUnit = TimeInterval.Week;
                    this.ChartLabelFormat = "MMM, dd";
                    break;
                case "6M":
                    this.FromDate = this.ToDate.AddMonths(-6);
                    this.ChartMajorStepUnit = TimeInterval.Month;
                    this.ChartLabelFormat = "MMM";
                    break;
                case "1Y":
                    this.FromDate = this.ToDate.AddYears(-1);
                    this.ChartMajorStepUnit = TimeInterval.Month;
                    this.ChartLabelFormat = "MMM";
                    break;
            }

            this.LoadCoinData();
        }

        private void OnChartTypeSelection()
        {
            this.IsLineChartVisible = this.SelectedChartType == LineChartType;
            this.IsCandlestickChartVisible = this.SelectedChartType == CandlestickChartType;

            this.LoadCoinData();
        }
    }
}
