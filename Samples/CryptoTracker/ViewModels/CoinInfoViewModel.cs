using System;
using System.Linq;
using System.Collections.Generic;
using CryptoTracker.Data;
using Microsoft.Maui.Controls;
using Telerik.Maui.Controls;
using Telerik.Maui.Controls.Compatibility.Chart;

namespace CryptoTracker.ViewModels
{
    public class CoinInfoViewModel : NotifyPropertyChangedBase
    {
        private const string dailySymbol = "1D";
        private const string weeklySymbol = "1W";
        private const string monthlySymbol = "1M";
        private const string halfYearlySymbol = "6M";
        private const string yearlySymbol = "1Y";
        private const int dataLimit = 365;

        private CoinData selectedCoin;
        private const int LineChartType = 0;
        private const int CandlestickChartType = 1;
        private double coinCurrentPrice;
        private string coinName;
        private string coinSymbol;
        private int countDays;
        private int selectedTimePeriod;
        private double coinPriceChangePercentage;
        private TimeInterval chartMajorStepUnit;
        private double chartMajorStep;
        private string chartLabelFormat;
        private string dataGridLabelFormat;
        private IList<CoinData> coinData;
        private IList<CoinData> candlestickChartData;
        private IList<CoinData> intervalCoinData;
        private IList<CoinData> hourlyCoinData;
        private IList<string> timePeriods;
        private bool isLineChartVisible;
        private bool isCandlestickChartVisible;
        private bool isChartLoading;
        private int selectedChartType;

        public CoinInfoViewModel()
        {
            this.ChartMajorStep = 1.0;
            this.CoinData = new List<CoinData>();
            this.CandlestickChartData = new List<CoinData>();
            this.IntervalCoinData = new List<CoinData>();
            this.hourlyCoinData = new List<CoinData>();
            this.TimePeriods = new List<string>() { dailySymbol, weeklySymbol, monthlySymbol, halfYearlySymbol, yearlySymbol };
            this.IsLineChartVisible = true;
            this.IsChartLoading = false;
        }

        public IList<CoinData> CoinData
        {
            get => this.coinData;
            private set => this.UpdateValue(ref this.coinData, value);
        }

        public IList<CoinData> CandlestickChartData
        {
            get => this.candlestickChartData;
            private set => this.UpdateValue(ref this.candlestickChartData, value);
        }

        public IList<CoinData> IntervalCoinData
        {
            get => this.intervalCoinData;
            private set => this.UpdateValue(ref this.intervalCoinData, value);
        }

        public bool IsLineChartVisible
        {
            get => this.isLineChartVisible;
            private set => this.UpdateValue(ref this.isLineChartVisible, value);
        }

        public bool IsChartLoading
        {
            get => this.isChartLoading;
            private set => this.UpdateValue(ref this.isChartLoading, value);
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

        public CoinData SelectedCoin
        {
            get => this.selectedCoin;
            private set => UpdateValue(ref this.selectedCoin, value);
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

        public int CountDays
        {
            get => this.countDays;
            private set => this.UpdateValue(ref this.countDays, value);
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

        public string DataGridLabelFormat
        {
            get => this.dataGridLabelFormat;
            private set => this.UpdateValue(ref this.dataGridLabelFormat, value);
        }

        public void InitializeCoinData(CoinData currentCoinInfo)
        {
            this.SelectedCoin = currentCoinInfo;
            this.CoinName = currentCoinInfo.Name;
            this.CoinSymbol = currentCoinInfo.Symbol;
            this.CoinCurrentPrice = currentCoinInfo.OpeningPrice;
            this.CoinPriceChangePercentage = currentCoinInfo.ChangeInPricePercentage;
            this.CountDays = 31;
            if (this.SelectedTimePeriod == 2)
            {
                this.OnDatePeriodSelection();
            }
            else
            {
                this.SelectedTimePeriod = 2;
            }
            this.SelectedTimePeriod = 2;

            if (this.SelectedChartType == 0)
            {
                this.OnChartTypeSelection();
            }
            else
            {
                this.SelectedChartType = 0;

            }

            this.DownLoadDailyCoinData(dataLimit);
            this.DownLoadHourlyCoinData();
        }

        private async void DownLoadDailyCoinData(int limitDays)
        {
            this.IsChartLoading = true;
            var coinService = DependencyService.Get<ICoinDataService>();
            this.CoinData = await coinService.GetOHLCCoinDataAsync(this.SelectedCoin, limitDays);
            this.LoadCoinData();
            this.IsChartLoading = false;
        }

        private async void DownLoadHourlyCoinData()
        {
            this.IsChartLoading = true;
            var coinService = DependencyService.Get<ICoinDataService>();
            this.hourlyCoinData = await coinService.GetHourlyOHLCCoinDataAsync(this.SelectedCoin);
            this.IsChartLoading = false;
        }

        private void LoadCoinData()
        {
            if (this.CoinData.Count == 0)
            {
                return;
            }

            if (this.CountDays == 1)
            {
                this.IntervalCoinData = hourlyCoinData;
                this.CandlestickChartData = this.Get24HourCandlestickData();
                return;
            }

            var currentData = new List<CoinData>();
            for (int i = this.CoinData.Count - 1; i >= this.CoinData.Count - this.CountDays; i--)
            {
                currentData.Add(this.CoinData[i]);
            }

            this.IntervalCoinData = currentData;
            this.LoadCandlestickData(currentData);
        }

        private void LoadCandlestickData(IList<CoinData> lineChartCoinData)
        {
            var newData = new List<CoinData>();
            newData.Add(lineChartCoinData[0]);

            var newOpen = 0.0;
            var newHigh = 0.0;
            var newLow = double.MaxValue;
            bool isNewElement = true;

            var previousDate = lineChartCoinData[0].Date;
            var nextDate = new DateTime();

            for (int i = 1; i < lineChartCoinData.Count; i++)
            {
                if (isNewElement)
                {
                    newOpen = lineChartCoinData[i].OpeningPrice;
                    isNewElement = false;
                }

                newHigh = lineChartCoinData[i].Price24High > newHigh ? lineChartCoinData[i].Price24High : newHigh;
                newLow = lineChartCoinData[i].Price24Low < newLow ? lineChartCoinData[i].Price24Low : newLow;

                var currentDate = lineChartCoinData[i].Date;
                switch (this.TimePeriods[this.SelectedTimePeriod])
                {
                    case weeklySymbol:
                        this.CandlestickChartData = lineChartCoinData;
                        return;
                    case monthlySymbol:
                        nextDate = previousDate.AddDays(-7);
                        break;
                    case halfYearlySymbol:
                    case yearlySymbol:
                        nextDate = previousDate.AddMonths(-1);
                        break;
                }

                if (currentDate.Date == nextDate.Date)
                {
                    newData.Add(new CoinData()
                    {
                        Name = lineChartCoinData[i].Name,
                        Symbol = lineChartCoinData[i].Symbol,
                        UnixTimeStamp = lineChartCoinData[i].UnixTimeStamp,
                        ClosingPrice = lineChartCoinData[i].ClosingPrice,
                        OpeningPrice = newOpen,
                        Price24Low = newLow,
                        Price24High = newHigh,
                    });

                    newHigh = 0;
                    newLow = double.MaxValue;
                    isNewElement = true;
                    previousDate = currentDate;
                }
            }
            this.CandlestickChartData = newData;
        }

        private IList<CoinData> Get24HourCandlestickData()
        {
            var newData = new List<CoinData>();
            for (int i = 0; i < this.hourlyCoinData.Count; i += (int)this.ChartMajorStep)
            {
                newData.Add(this.hourlyCoinData[i]);
            }

            return newData;
        }

        private void OnDatePeriodSelection()
        {
            var timePeriod = this.TimePeriods[this.SelectedTimePeriod];

            this.ChartMajorStep = 1;
            this.ChartMajorStepUnit = TimeInterval.Month;
            this.ChartLabelFormat = "MMM";
            this.DataGridLabelFormat = "{0:MMM d, yyyy}";

            switch (timePeriod)
            {
                case dailySymbol:
                    this.CountDays = 1;
                    this.ChartMajorStepUnit = TimeInterval.Hour;
                    this.ChartLabelFormat = "h tt";
                    this.DataGridLabelFormat = "{0:MMM d, yyyy, h tt}";
                    this.ChartMajorStep = 4;
                    break;
                case weeklySymbol:
                    this.CountDays = 7;
                    this.ChartMajorStepUnit = TimeInterval.Day;
                    this.ChartLabelFormat = "ddd";
                    break;
                case monthlySymbol:
                    this.CountDays = 31;
                    this.ChartMajorStepUnit = TimeInterval.Week;
                    this.ChartLabelFormat = "MMM, dd";
                    break;
                case halfYearlySymbol:
                    this.CountDays = 182;
                    break;
                case yearlySymbol:
                    this.CountDays = 365;
                    break;
            }

            this.LoadCoinData();
        }

        private void OnChartTypeSelection()
        {
            this.IsLineChartVisible = this.SelectedChartType == LineChartType;
            this.IsCandlestickChartVisible = this.SelectedChartType == CandlestickChartType;
        }
    }
}
