using QSF.ViewModels; 
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace QSF.Examples.ChartsControl.LineSeriesExample;

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

        return new GalleryItemViewModelBase[]
        {
            new SeriesGalleryItemViewModel("chartline1headeractive.png", "chartline1headerinactive.png", "line", seriesData),
        };
    }
}
