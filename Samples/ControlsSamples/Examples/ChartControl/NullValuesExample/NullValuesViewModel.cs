using QSF.ViewModels;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace QSF.Examples.ChartControl.NullValuesExample;

public class NullValuesViewModel : GalleryExampleViewModelBase
{
    protected override IEnumerable<GalleryItemViewModelBase> GetGalleryItems()
    {
        var seriesData = new ObservableCollection<DataItem>()
        {
            new DataItem { Category = "Greenings", Value = 5 },
            new DataItem { Category = "Perfecto", Value = 15 },
            new DataItem { Category = "NearBy", Value = 5 },
            new DataItem { Category = "Stop&Shop", Value = 25 },
            new DataItem { Category = "Other", },
            new DataItem { Category = "Lidl", Value = 35 },
            new DataItem { Category = "FamilyStore", Value = 45 },
            new DataItem { Category = "Fresh&Green", Value = 10 }
        };

        var secondSeriesData = new ObservableCollection<DataItem>()
        {
            new DataItem { Category = "Greenings", Value = 60 },
            new DataItem { Category = "Perfecto", Value = 47 },
            new DataItem { Category = "NearBy", Value = 51 },
            new DataItem { Category = "Stop&Shop", Value = 32 },
            new DataItem { Category = "Other", },
            new DataItem { Category = "Lidl", Value = 43 },
            new DataItem { Category = "FamilyStore", Value = 26 },
            new DataItem { Category = "Fresh&Green", Value = 82 }
        };

        return new GalleryItemViewModelBase[]
        {
            new SeriesGalleryItemViewModel("chartline1headeractive.png", "chartline1headerinactive.png", "line", seriesData),
            new SeriesGalleryItemViewModel("chartline2headeractive.png", "chartline2headerinactive.png", "stackedLine", seriesData, secondSeriesData),
            new SeriesGalleryItemViewModel("chartline3headeractive.png", "chartline3headerinactive.png", "spline", seriesData),
            new SeriesGalleryItemViewModel("chartline4headeractive.png", "chartline4headerinactive.png", "stackedSpline", seriesData, secondSeriesData),
            new SeriesGalleryItemViewModel("chartarea1headeractive.png", "chartarea1headerinactive.png", "lineArea", seriesData),
            new SeriesGalleryItemViewModel("chartarea2headeractive.png", "chartarea2headerinactive.png", "stackedLineArea", seriesData, secondSeriesData),
            new SeriesGalleryItemViewModel("chartarea3headeractive.png", "chartarea3headerinactive.png", "splineArea", seriesData),
            new SeriesGalleryItemViewModel("chartarea4headeractive.png", "chartarea4headerinactive.png", "stackedSplineArea", seriesData, secondSeriesData),
        };
    }
}
