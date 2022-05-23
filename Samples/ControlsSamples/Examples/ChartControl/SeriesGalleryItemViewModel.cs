using QSF.ViewModels;
using System.Collections.ObjectModel;

namespace QSF.Examples.ChartControl
{
    public class SeriesGalleryItemViewModel : GalleryItemViewModelBase
    {
        public SeriesGalleryItemViewModel(string icon, string inactiveIcon, string dataTemplateResourceKey, ObservableCollection<DataItem> seriesData, ObservableCollection<DataItem> secondSeriesData = null, ObservableCollection<DataItem> thirdSeriesData = null)
            : base(icon, inactiveIcon, dataTemplateResourceKey)
        {
            this.SeriesData = seriesData;
            this.SecondSeriesData = secondSeriesData;
            this.ThirdSeriesData = thirdSeriesData;
        }

        public ObservableCollection<DataItem> SeriesData { get; }

        public ObservableCollection<DataItem> SecondSeriesData { get; }

        public ObservableCollection<DataItem> ThirdSeriesData { get; }
    }
}