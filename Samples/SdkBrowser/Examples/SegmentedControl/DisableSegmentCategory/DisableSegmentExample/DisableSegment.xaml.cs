using Microsoft.Maui.Controls;

namespace SDKBrowserMaui.Examples.SegmentedControl.DisableSegmentCategory.DisableSegmentExample;

public partial class DisableSegment : ContentView
{
	public DisableSegment()
	{
		InitializeComponent();
        // >> segmentcontrol-disablesegment-setsegmentenabled
        this.segmentControl.SetSegmentEnabled(2, false);
        // << segmentcontrol-disablesegment-setsegmentenabled
    }
}