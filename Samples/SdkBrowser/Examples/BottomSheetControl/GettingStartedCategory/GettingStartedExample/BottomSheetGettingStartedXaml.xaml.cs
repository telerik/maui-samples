using Microsoft.Maui.Controls;
using Telerik.Maui.Controls.BottomSheet;

namespace SDKBrowserMaui.Examples.BottomSheetControl.GettingStartedCategory.GettingStartedExample;

public partial class BottomSheetGettingStartedXaml : ContentView
{
	public BottomSheetGettingStartedXaml()
	{
		InitializeComponent();
	}

	// >> open-bottomsheet-view
	private void OnOpenClicked(object sender, System.EventArgs e)
	{
		this.bottomSheet.GoToBottomSheetState(BottomSheetState.PartialStateName);
	}
	// << open-bottomsheet-view

	// >> close-bottomsheet-view
	private void OnCloseClicked(object sender, System.EventArgs e)
	{
		this.bottomSheet.GoToBottomSheetState(BottomSheetState.HiddenStateName);
	}
	// << close-bottomsheet-view
}