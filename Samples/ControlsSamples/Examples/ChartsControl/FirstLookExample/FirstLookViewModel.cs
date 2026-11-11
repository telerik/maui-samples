using QSF.ViewModels;
using Microsoft.Maui.Graphics;
using System.Collections.Generic;

namespace QSF.Examples.ChartsControl.FirstLookExample;

public class FirstLookViewModel : ExampleViewModel
{
    private static readonly string[] Months = { "Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec" };

    public FirstLookViewModel()
    {
        this.Kpis = new List<KpiItem>
        {
            new KpiItem
            {
                Title = "Revenue", Value = "$1.24M", Trend = "↑ +8.2%", IsPositive = true,
                IconData = "M4,6 A8,3 0 1 0 20,6 A8,3 0 1 0 4,6 M4,6 L4,18 A8,3 0 0 0 20,18 L20,6 M4,12 A8,3 0 0 0 20,12",
                IconColor = Color.FromArgb("#1E6FE8"), IconBackgroundColor = Color.FromArgb("#331E6FE8"),
            },
            new KpiItem
            {
                Title = "Orders", Value = "8,430", Trend = "↑ +5.1%", IsPositive = true,
                IconData = "M2,3 L5,3 L8,15 L19,15 L21,7 L6,7 M8,19 L10,19 L10,21 L8,21 Z M16,19 L18,19 L18,21 L16,21 Z",
                IconColor = Color.FromArgb("#1E6FE8"), IconBackgroundColor = Color.FromArgb("#331E6FE8"),
            },
            new KpiItem
            {
                Title = "Conversion", Value = "3.6%", Trend = "↓ −0.4%", IsPositive = false,
                IconData = "M3,4 L21,4 L14,12 L14,19 L10,21 L10,12 Z",
                IconColor = Color.FromArgb("#8E44D9"), IconBackgroundColor = Color.FromArgb("#338E44D9"),
            },
            new KpiItem
            {
                Title = "Avg. Order", Value = "$147", Trend = "↑ +2.7%", IsPositive = true,
                IconData = "M3,3 L11,3 L21,13 L13,21 L3,11 Z M6,6 L8,6 L8,8 L6,8 Z",
                IconColor = Color.FromArgb("#E8961E"), IconBackgroundColor = Color.FromArgb("#33E8961E"),
            },
        };

        this.RevenueCosts = CreateMonthly(0.52, 0.55, 0.58, 0.62, 0.64, 0.66, 0.67, 0.70, 0.72, 0.74, 0.76, 0.82);
        this.RevenueTarget = CreateMonthly(0.86, 0.91, 0.97, 1.02, 1.05, 1.10, 1.17, 1.21, 1.25, 1.26, 1.36, 1.42);
        this.Revenue = CreateMonthly(null, null, null, null, null, null, null, 1.21, 1.32, 1.38, 1.48, 1.58);

        this.Channels = new List<DataItem>
        {
            new DataItem { Category = "Online (Direct)", Value = 42 },
            new DataItem { Category = "Marketplace", Value = 24 },
            new DataItem { Category = "Retail", Value = 16 },
            new DataItem { Category = "Partner", Value = 12 },
            new DataItem { Category = "Other", Value = 6 },
        };
        this.ChannelsLegend = new List<LegendItem>
        {
            new LegendItem { Name = "Online (Direct)", Value = "$520K", Share = "42.0%", PaletteIndex = 0 },
            new LegendItem { Name = "Marketplace", Value = "$298K", Share = "24.0%", PaletteIndex = 1 },
            new LegendItem { Name = "Retail", Value = "$198K", Share = "16.0%", PaletteIndex = 2 },
            new LegendItem { Name = "Partner", Value = "$149K", Share = "12.0%", PaletteIndex = 3 },
            new LegendItem { Name = "Other", Value = "$74K", Share = "6.0%", PaletteIndex = 4 },
        };

        this.RegionNorthAmerica = CreateQuarterly(0.39, 0.41, 0.45, 0.50);
        this.RegionEurope = CreateQuarterly(0.25, 0.27, 0.29, 0.32);
        this.RegionAsiaPacific = CreateQuarterly(0.19, 0.21, 0.22, 0.25);
        this.RegionLatinAmerica = CreateQuarterly(0.15, 0.16, 0.17, 0.17);
        this.RegionsLegend = new List<LegendItem>
        {
            new LegendItem { Name = "North America", Value = "$1.75M", PaletteIndex = 0 },
            new LegendItem { Name = "Europe", Value = "$1.13M", PaletteIndex = 1 },
            new LegendItem { Name = "Asia Pacific", Value = "$0.87M", PaletteIndex = 2 },
            new LegendItem { Name = "Latin America", Value = "$0.65M", PaletteIndex = 3 },
        };

        this.CampaignsSearch = new List<DataItem>
        {
            new DataItem { Value = 50, ValueY = 250 }, new DataItem { Value = 70, ValueY = 215 },
            new DataItem { Value = 100, ValueY = 305 }, new DataItem { Value = 110, ValueY = 270 },
            new DataItem { Value = 115, ValueY = 190 }, new DataItem { Value = 150, ValueY = 350 },
            new DataItem { Value = 185, ValueY = 455 }, new DataItem { Value = 210, ValueY = 420 },
            new DataItem { Value = 235, ValueY = 505 }, new DataItem { Value = 270, ValueY = 620 },
            new DataItem { Value = 325, ValueY = 685 }, new DataItem { Value = 375, ValueY = 740 },
        };
        this.CampaignsSocial = new List<DataItem>
        {
            new DataItem { Value = 25, ValueY = 120 }, new DataItem { Value = 45, ValueY = 95 },
            new DataItem { Value = 70, ValueY = 135 }, new DataItem { Value = 85, ValueY = 160 },
            new DataItem { Value = 100, ValueY = 180 }, new DataItem { Value = 145, ValueY = 250 },
            new DataItem { Value = 150, ValueY = 190 }, new DataItem { Value = 165, ValueY = 225 },
            new DataItem { Value = 225, ValueY = 325 }, new DataItem { Value = 270, ValueY = 380 },
            new DataItem { Value = 305, ValueY = 430 },
        };

        this.ProductMix = new List<DataItem>
        {
            new DataItem { Category = "Premium Suite", Value = 28, Label = "28%" },
            new DataItem { Category = "Pro Plan", Value = 24, Label = "24%" },
            new DataItem { Category = "Standard Plan", Value = 18, Label = "18%" },
            new DataItem { Category = "Basic Plan", Value = 12, Label = "12%" },
            new DataItem { Category = "Add-ons", Value = 10, Label = "10%" },
            new DataItem { Category = "Services", Value = 8, Label = "8%" },
        };

        this.Traffic = new List<DataItem>
        {
            new DataItem { Category = "Mon", Value = 4.2 },
            new DataItem { Category = "Tue", Value = 5.1 },
            new DataItem { Category = "Wed", Value = 4.8 },
            new DataItem { Category = "Thu", Value = 5.6 },
            new DataItem { Category = "Fri", Value = 6.3 },
            new DataItem { Category = "Sat", Value = 7.4 },
            new DataItem { Category = "Sun", Value = 6.9 },
        };

        this.NewCustomers = CreateQuarterly(12, 18, 22, 27);
        this.ReturningCustomers = CreateQuarterly(20, 24, 29, 35);

        this.StockPrices = new List<FinancialDataItem>
        {
            new FinancialDataItem { Category = "W1", Open = 102, High = 108, Low = 99, Close = 106 },
            new FinancialDataItem { Category = "W2", Open = 106, High = 111, Low = 103, Close = 104 },
            new FinancialDataItem { Category = "W3", Open = 104, High = 109, Low = 100, Close = 108 },
            new FinancialDataItem { Category = "W4", Open = 108, High = 115, Low = 106, Close = 113 },
            new FinancialDataItem { Category = "W5", Open = 113, High = 116, Low = 107, Close = 109 },
            new FinancialDataItem { Category = "W6", Open = 109, High = 114, Low = 105, Close = 112 },
            new FinancialDataItem { Category = "W7", Open = 112, High = 120, Low = 111, Close = 118 },
            new FinancialDataItem { Category = "W8", Open = 118, High = 122, Low = 113, Close = 115 },
            new FinancialDataItem { Category = "W9", Open = 115, High = 119, Low = 111, Close = 117 },
            new FinancialDataItem { Category = "W10", Open = 117, High = 123, Low = 115, Close = 121 },
            new FinancialDataItem { Category = "W11", Open = 121, High = 124, Low = 116, Close = 118 },
            new FinancialDataItem { Category = "W12", Open = 118, High = 121, Low = 112, Close = 114 },
            new FinancialDataItem { Category = "W13", Open = 114, High = 118, Low = 110, Close = 116 },
            new FinancialDataItem { Category = "W14", Open = 116, High = 121, Low = 114, Close = 119 },
            new FinancialDataItem { Category = "W15", Open = 119, High = 124, Low = 117, Close = 122 },
            new FinancialDataItem { Category = "W16", Open = 122, High = 125, Low = 118, Close = 120 },
            new FinancialDataItem { Category = "W17", Open = 120, High = 123, Low = 116, Close = 117 },
            new FinancialDataItem { Category = "W18", Open = 117, High = 121, Low = 114, Close = 119 },
            new FinancialDataItem { Category = "W19", Open = 119, High = 124, Low = 118, Close = 123 },
            new FinancialDataItem { Category = "W20", Open = 123, High = 126, Low = 119, Close = 121 },
            new FinancialDataItem { Category = "W21", Open = 121, High = 124, Low = 116, Close = 118 },
            new FinancialDataItem { Category = "W22", Open = 118, High = 120, Low = 113, Close = 115 },
            new FinancialDataItem { Category = "W23", Open = 115, High = 119, Low = 112, Close = 118 },
            new FinancialDataItem { Category = "W24", Open = 118, High = 123, Low = 117, Close = 121 },
            new FinancialDataItem { Category = "W25", Open = 121, High = 125, Low = 119, Close = 124 },
            new FinancialDataItem { Category = "W26", Open = 124, High = 126, Low = 120, Close = 122 },
            new FinancialDataItem { Category = "W27", Open = 122, High = 124, Low = 117, Close = 119 },
            new FinancialDataItem { Category = "W28", Open = 119, High = 122, Low = 115, Close = 116 },
            new FinancialDataItem { Category = "W29", Open = 116, High = 120, Low = 113, Close = 118 },
            new FinancialDataItem { Category = "W30", Open = 118, High = 123, Low = 117, Close = 122 },
            new FinancialDataItem { Category = "W31", Open = 122, High = 125, Low = 120, Close = 121 },
            new FinancialDataItem { Category = "W32", Open = 121, High = 123, Low = 116, Close = 117 },
            new FinancialDataItem { Category = "W33", Open = 117, High = 121, Low = 115, Close = 120 },
            new FinancialDataItem { Category = "W34", Open = 120, High = 124, Low = 118, Close = 123 },
            new FinancialDataItem { Category = "W35", Open = 123, High = 126, Low = 121, Close = 124 },
            new FinancialDataItem { Category = "W36", Open = 124, High = 125, Low = 119, Close = 120 },
            new FinancialDataItem { Category = "W37", Open = 120, High = 122, Low = 116, Close = 118 },
            new FinancialDataItem { Category = "W38", Open = 118, High = 121, Low = 114, Close = 116 },
            new FinancialDataItem { Category = "W39", Open = 116, High = 120, Low = 115, Close = 119 },
            new FinancialDataItem { Category = "W40", Open = 119, High = 123, Low = 118, Close = 122 },
            new FinancialDataItem { Category = "W41", Open = 122, High = 125, Low = 120, Close = 121 },
            new FinancialDataItem { Category = "W42", Open = 121, High = 124, Low = 118, Close = 123 },
            new FinancialDataItem { Category = "W43", Open = 123, High = 126, Low = 120, Close = 122 },
            new FinancialDataItem { Category = "W44", Open = 122, High = 125, Low = 119, Close = 124 },
        };
    }

    public IList<KpiItem> Kpis { get; }

    public IList<DataItem> Revenue { get; }

    public IList<DataItem> RevenueTarget { get; }

    public IList<DataItem> RevenueCosts { get; }

    public IList<DataItem> Channels { get; }

    public IList<LegendItem> ChannelsLegend { get; }

    public IList<DataItem> RegionNorthAmerica { get; }

    public IList<DataItem> RegionEurope { get; }

    public IList<DataItem> RegionAsiaPacific { get; }

    public IList<DataItem> RegionLatinAmerica { get; }

    public IList<LegendItem> RegionsLegend { get; }

    public IList<DataItem> CampaignsSocial { get; }

    public IList<DataItem> CampaignsSearch { get; }

    public IList<DataItem> ProductMix { get; }

    public IList<DataItem> Traffic { get; }

    public IList<DataItem> NewCustomers { get; }

    public IList<DataItem> ReturningCustomers { get; }

    public IList<FinancialDataItem> StockPrices { get; }

    private static IList<DataItem> CreateMonthly(params double?[] values)
    {
        var items = new List<DataItem>();
        for (int i = 0; i < values.Length; i++)
        {
            items.Add(new DataItem { Category = Months[i], Value = values[i] });
        }

        return items;
    }

    private static IList<DataItem> CreateQuarterly(params double[] values)
    {
        var items = new List<DataItem>();
        for (int i = 0; i < values.Length; i++)
        {
            items.Add(new DataItem { Category = "Q" + (i + 1), Value = values[i] });
        }

        return items;
    }
}
