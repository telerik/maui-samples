using Microsoft.Maui.Controls;
using Telerik.Maui.Controls.BottomSheet;

namespace SDKBrowserMaui.Examples.BottomSheetControl.FeaturesCategory.CustomControlTemplateExample;

public partial class CustomControlTemplate : ContentView
{
	public CustomControlTemplate()
	{
		InitializeComponent();
	}

	private void OnOpenClicked(object sender, System.EventArgs e)
	{
		this.bottomSheet.GoToBottomSheetState(BottomSheetState.FullStateName);
	}

	private void OnCloseClicked(object sender, System.EventArgs e)
	{
		this.bottomSheet.GoToBottomSheetState(BottomSheetState.HiddenStateName);
	}
}