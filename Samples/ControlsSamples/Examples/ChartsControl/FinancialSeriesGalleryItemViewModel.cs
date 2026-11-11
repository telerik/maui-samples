using QSF.ViewModels;

namespace QSF.Examples.ChartsControl;

public class FinancialSeriesGalleryItemViewModel : GalleryItemViewModelBase
{
    public FinancialSeriesGalleryItemViewModel(string icon, string inactiveIcon, string dataTemplateResourceKey, FinancialDataItem[] seriesData)
        : base(icon, inactiveIcon, dataTemplateResourceKey)
    {
        this.SeriesData = seriesData;
    }

    public FinancialDataItem[] SeriesData { get; }
}
