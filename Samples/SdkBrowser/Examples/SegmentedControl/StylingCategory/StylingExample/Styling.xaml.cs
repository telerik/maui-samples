using Microsoft.Maui.Controls;

namespace SDKBrowserMaui.Examples.SegmentedControl.StylingCategory.StylingExample;

public partial class Styling : ContentView
{
	public Styling()
	{
		InitializeComponent();
        this.segmentControl.SetSegmentEnabled(2, false);
    }
}