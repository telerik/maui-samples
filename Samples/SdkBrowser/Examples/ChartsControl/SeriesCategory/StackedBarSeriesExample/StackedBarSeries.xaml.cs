using Microsoft.Maui.Controls;
using Telerik.Maui.Controls;
using Telerik.Maui.Controls.Charts;

namespace SDKBrowserMaui.Examples.ChartsControl.SeriesCategory.StackedBarSeriesExample;

public partial class StackedBarSeries : ContentView
{
	public StackedBarSeries()
	{
		InitializeComponent();
	}

	private void OnCombineModeSelectionChanged(object sender, ComboBoxSelectionChangedEventArgs e)
	{
		if (this.combineModeComboBox.SelectedItem is ChartBarCombineMode combineMode)
		{
			this.barseriesFirst.CombineMode = combineMode;
			this.barseriesSecond.CombineMode = combineMode;
		}
	}
}