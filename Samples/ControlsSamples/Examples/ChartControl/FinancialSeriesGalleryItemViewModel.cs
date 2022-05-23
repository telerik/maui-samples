using QSF.ViewModels;

namespace QSF.Examples.ChartControl;

public class FinancialSeriesGalleryItemViewModel : GalleryItemViewModelBase
{
    public FinancialSeriesGalleryItemViewModel(string icon, string inactiveIcon, string dataTemplateResourceKey, FinancialDataItem[] seriesData, FinancialDataItem[] secondSeriesData = null, FinancialDataItem[] thirdSeriesData = null) 
        : base(icon, inactiveIcon, dataTemplateResourceKey)
    {
        this.SeriesData = seriesData;
        this.SecondSeriesData = secondSeriesData;
        this.ThirdSeriesData = thirdSeriesData;
    }

    public FinancialDataItem[] SeriesData { get; }

    public FinancialDataItem[] SecondSeriesData { get; }

    public FinancialDataItem[] ThirdSeriesData { get; }
}