using QSF.ViewModels;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace QSF.Examples.ChartControl.BarSeriesExample
{
    internal class BarSeriesViewModel : GalleryExampleViewModelBase
    {
        protected override IEnumerable<GalleryItemViewModelBase> GetGalleryItems()
        {
            var seriesData = new ObservableCollection<DataItem>()
            {
                new DataItem(){Category = "Greenings", Value = 85},
                new DataItem(){Category = "Perfecto", Value = 78},
                new DataItem(){Category = "NearBy", Value = 99},
                new DataItem(){Category = "FamilyStore", Value = 85},
                new DataItem(){Category = "Fresh&Green", Value = 57}
            };

            var secondSeriesData = new ObservableCollection<DataItem>()
            {
                new DataItem(){Category = "Greenings", Value = 75},
                new DataItem(){Category = "Perfecto", Value = 95},
                new DataItem(){Category = "NearBy", Value = 91},
                new DataItem(){Category = "FamilyStore", Value = 64},
                new DataItem(){Category = "Fresh&Green", Value = 67}
            };

            return new GalleryItemViewModelBase[]
            {
                new SeriesGalleryItemViewModel("chartbar1headeractive.png", "chartbar1headerinactive.png", "bar", seriesData),
                new SeriesGalleryItemViewModel("chartbar2headeractive.png", "chartbar2headerinactive.png", "cluster", seriesData, secondSeriesData),
                new SeriesGalleryItemViewModel("chartbar3headeractive.png", "chartbar3headerinactive.png", "stacked", seriesData, secondSeriesData),
                new SeriesGalleryItemViewModel("chartbar4headeractive.png", "chartbar4headerinactive.png", "stacked100", seriesData, secondSeriesData),
                new SeriesGalleryItemViewModel("chartbar5headeractive.png", "chartbar5headerinactive.png", "horBar", seriesData),
                new SeriesGalleryItemViewModel("chartbar6headeractive.png", "chartbar6headerinactive.png", "horCluster", seriesData, secondSeriesData),
                new SeriesGalleryItemViewModel("chartbar7headeractive.png", "chartbar7headerinactive.png", "horStacked", seriesData, secondSeriesData),
                new SeriesGalleryItemViewModel("chartbar8headeractive.png", "chartbar8headerinactive.png", "horStacked100", seriesData, secondSeriesData),
            };
        }
    }
}