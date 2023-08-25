using QSF.ViewModels;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.Json;
using System.Text.Json.Serialization.Metadata;

namespace QSF.Examples.ChartControl.FinancialSeriesExample;

public class FinancialSeriesViewModel : GalleryExampleViewModelBase
{
    protected override IEnumerable<GalleryItemViewModelBase> GetGalleryItems()
    {
        var seriesData = this.LoadDataFromJsonFile();

        return new GalleryItemViewModelBase[]
        {
           new FinancialSeriesGalleryItemViewModel("chartfinancial1headeractive.png", "chartfinancial1headerinactive.png", "OhlcSeries", seriesData),
           new FinancialSeriesGalleryItemViewModel("chartfinancial2headeractive.png", "chartfinancial2headerinactive.png", "CandlestickSeries", seriesData),
           new FinancialSeriesGalleryItemViewModel("chartfinancial3headeractive.png", "chartfinancial3headerinactive.png", "FinancialIndicators", seriesData)
        };
    }

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

        return financialData.Take(32).ToArray();
    }
}