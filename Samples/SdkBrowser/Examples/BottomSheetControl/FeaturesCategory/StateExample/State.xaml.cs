using Microsoft.Maui.Controls;
using Telerik.Maui.Controls.BottomSheet;

namespace SDKBrowserMaui.Examples.BottomSheetControl.FeaturesCategory.StateExample;

public partial class State : ContentView
{
	public State()
	{
		InitializeComponent();
	}

	private void OnFullClicked(object sender, System.EventArgs e)
	{
		this.bottomSheet.GoToBottomSheetState(BottomSheetState.FullStateName);
	}
	private void OnHiddenClicked(object sender, System.EventArgs e)
	{
		this.bottomSheet.GoToBottomSheetState(BottomSheetState.HiddenStateName);
	}

	private void OnPartialClicked(object sender, System.EventArgs e)
	{
		this.bottomSheet.GoToBottomSheetState(BottomSheetState.PartialStateName);
	}

	private void OnMinimalClicked(object sender, System.EventArgs e)
	{
		this.bottomSheet.GoToBottomSheetState(BottomSheetState.MinimalStateName);
	}
}