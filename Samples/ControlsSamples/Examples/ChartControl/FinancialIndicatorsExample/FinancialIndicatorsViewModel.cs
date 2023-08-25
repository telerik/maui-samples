using QSF.Examples.ChartControl.FinancialSeriesExample;
using QSF.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.Json;
using System.Text.Json.Serialization.Metadata;

namespace QSF.Examples.ChartControl.FinancialIndicatorsExample;

public class FinancialIndicatorsViewModel : ExampleViewModel
{
    public FinancialIndicatorsViewModel()
    {
        this.IndicatorsList = Enum.GetValues(typeof(Indicators)).Cast<Indicators>().ToList();
        this.TrendlinesList = Enum.GetValues(typeof(Trendlines)).Cast<Trendlines>().ToList();
        this.SeriesData = this.LoadDataFromJsonFile();
    }

    public List<Indicators> IndicatorsList { get; set; }

    public List<Trendlines> TrendlinesList { get; set; }

    public FinancialDataItem[] SeriesData { get; }

    private FinancialDataItem[] LoadDataFromJsonFile()
    {
        var assembly = IntrospectionExtensions.GetTypeInfo(typeof(FinancialSeriesViewModel)).Assembly;
        Stream stream = assembly.GetManifestResourceStream("QSF.Examples.ChartControl.FinancialSeriesExample.AppleStockPrices.json");
        FinancialDataItem[] financialData;

        using (var reader = new StreamReader(stream))
        {
            var json = reader.ReadToEnd();
            var options = new JsonSerializerOptions(JsonSerializerDefaults.Web) { TypeInfoResolver = new DefaultJsonTypeInfoResolver() };
            financialData = JsonSerializer.Deserialize<FinancialDataItem[]>(json, options);
        }

        return financialData;
    }
}