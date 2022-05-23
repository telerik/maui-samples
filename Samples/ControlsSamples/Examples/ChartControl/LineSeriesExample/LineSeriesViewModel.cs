using QSF.ViewModels;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace QSF.Examples.ChartControl.LineSeriesExample;

public class LineSeriesViewModel : GalleryExampleViewModelBase
{
    protected override IEnumerable<GalleryItemViewModelBase> GetGalleryItems()
    {
        var seriesData = new ObservableCollection<DataItem>()
        {
            new DataItem(){Category = "Greenings", Value = 5},
            new DataItem(){Category = "Perfecto", Value = 15},
            new DataItem(){Category = "NearBy", Value = 4},
            new DataItem(){Category = "FamilyStore", Value = 45},
            new DataItem(){Category = "Fresh&Green", Value = 10}
        };

        var secondSeriesData = new ObservableCollection<DataItem>()
        {
            new DataItem(){Category = "Greenings", Value = 60},
            new DataItem(){Category = "Perfecto", Value = 47},
            new DataItem(){Category = "NearBy", Value = 51},
            new DataItem(){Category = "FamilyStore", Value = 26},
            new DataItem(){Category = "Fresh&Green", Value = 82}
        };

        return new GalleryItemViewModelBase[]
        {
            new SeriesGalleryItemViewModel("chartline1headeractive.png", "chartline1headerinactive.png", "line", seriesData),
            new SeriesGalleryItemViewModel("chartline2headeractive.png", "chartline2headerinactive.png", "stackedLine", seriesData, secondSeriesData),
            new SeriesGalleryItemViewModel("chartline3headeractive.png", "chartline3headerinactive.png", "spline", seriesData),
            new SeriesGalleryItemViewModel("chartline4headeractive.png", "chartline4headerinactive.png", "stackedSpline", seriesData, secondSeriesData),
        };
    }
}