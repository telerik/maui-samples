using QSF.ViewModels;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace QSF.Examples.ChartControl.PieDonutSeriesExample;

public class PieDonutSeriesViewModel : GalleryExampleViewModelBase
{
    public PieDonutSeriesViewModel()
    {
    }
    protected override IEnumerable<GalleryItemViewModelBase> GetGalleryItems()
    {
        var seriesData = new ObservableCollection<DataItem>()
        {
            new DataItem() { Value = 60, Category = "France - 60%" },
            new DataItem() { Value = 40, Category = "Belguim - 40%" }
        };

        var secondSeriesData = new ObservableCollection<DataItem>()
        {
            new DataItem() { Value = 30, Category = "France - 30%" },
            new DataItem() { Value = 30, Category = "Germany - 30%" },
            new DataItem() { Value = 40, Category = "Belguim - 40%" }
        };

        return new GalleryItemViewModelBase[]
        {
            new SeriesGalleryItemViewModel("chartpie1headeractive.png", "chartpie1headerinactive.png", "Pie", seriesData),
            new SeriesGalleryItemViewModel("chartpie2headeractive.png", "chartpie2headerinactive.png", "Pie2", secondSeriesData),
            new SeriesGalleryItemViewModel("chartdonut1headeractive.png", "chartdonut1headerinactive.png", "Donut", seriesData)
        };
    }
}