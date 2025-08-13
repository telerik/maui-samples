using Microsoft.Maui.Controls;
using System;
using Telerik.Maui.Controls.BottomSheet;

namespace SDKBrowserMaui.Examples.BottomSheetControl.FeaturesCategory.SettingWidthExample;

public partial class SettingWidth : ContentView
{
	public SettingWidth()
	{
		InitializeComponent();

		this.Loaded += this.OnLoaded;
    }

    private void OnLoaded(object sender, EventArgs e)
    {
        this.bottomSheet.GoToBottomSheetState(BottomSheetState.PartialStateName);
    }

    // >> open-bottomsheet-view-width
    private void OnOpenClicked(object sender, System.EventArgs e)
	{
		this.bottomSheet.BottomSheetContentWidth = new BottomSheetLength(400, false);
	}
	// << open-bottomsheet-view-width

	// >> open-bottomsheet-view-percentage
	private void OnOpenPercentageClicked(object sender, System.EventArgs e)
	{
		this.bottomSheet.BottomSheetContentWidth = new BottomSheetLength(50, true);
	}
	// << open-bottomsheet-view-percentage
}