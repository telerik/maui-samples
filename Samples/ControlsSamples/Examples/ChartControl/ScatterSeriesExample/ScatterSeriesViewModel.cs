using QSF.ViewModels;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace QSF.Examples.ChartControl.ScatterSeriesExample;

public class ScatterSeriesViewModel : GalleryExampleViewModelBase
{
    protected override IEnumerable<GalleryItemViewModelBase> GetGalleryItems()
    {
        var seriesData = new ObservableCollection<DataItem>()
        {
            new DataItem(){Value = 20, ValueY = 300},
            new DataItem(){Value = 35, ValueY = 700},
            new DataItem(){Value = 60, ValueY = 950},
            new DataItem(){Value = 85, ValueY = 1000},
            new DataItem(){Value = 90, ValueY = 1050}
        };

        var secondSeriesData = new ObservableCollection<DataItem>()
        {
            new DataItem(){Value = 20, ValueY = 100},
            new DataItem(){Value = 40, ValueY = 700},
            new DataItem(){Value = 50, ValueY = 850},
            new DataItem(){Value = 95, ValueY = 900},
            new DataItem(){Value = 100, ValueY = 1250}
        };

        return new GalleryItemViewModelBase[]
        {
            new SeriesGalleryItemViewModel("chartscattered1headeractive.png", "chartscattered1headerinactive.png", "ScatterPointSeries", seriesData, secondSeriesData),
            new SeriesGalleryItemViewModel("chartscattered2headeractive.png", "chartscattered2headerinactive.png", "ScatterLineSeries", seriesData),
            new SeriesGalleryItemViewModel("chartscattered3headeractive.png", "chartscattered3headerinactive.png", "ScatterSplineAreaSeries", seriesData),
            new SeriesGalleryItemViewModel("chartscattered4headeractive.png", "chartscattered4headerinactive.png", "ScatterSplineSeries", seriesData),
            new SeriesGalleryItemViewModel("chartscattered5headeractive.png", "chartscattered5headerinactive.png", "ScatterAreaSeries", seriesData),
        };
    }
}