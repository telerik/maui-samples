using QSF.ViewModels;

namespace QSF.Examples.PdfViewerControl.ConfigurationExample;

public class ConfigurationViewModel : ConfigurationExampleViewModel
{
    private double pageSpacing = 20.0;
    private double minZoomLevel = 0.3;
    private double maxZoomLevel = 3.0;

    public double PageSpacing
    {
        get => this.pageSpacing;
        set => this.UpdateValue(ref this.pageSpacing, value);
    }

    public double MinZoomLevel
    {
        get => this.minZoomLevel;
        set => this.UpdateValue(ref this.minZoomLevel, value);
    }

    public double MaxZoomLevel
    {
        get => this.maxZoomLevel;
        set => this.UpdateValue(ref this.maxZoomLevel, value);
    }
}