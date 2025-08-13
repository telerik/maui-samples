using Microsoft.Maui.Controls;
using Telerik.Maui.Controls;
using Telerik.Maui.Controls.BottomSheet;

namespace SDKBrowserMaui.Examples.BottomSheetControl.GettingStartedCategory.GettingStartedExample;

public class BottomSheetGettingStartedCSharp : ContentView
{
	RadBottomSheet bottomSheet = new RadBottomSheet();

	public BottomSheetGettingStartedCSharp()
	{
		// >> bottomsheet-getting-started-csharp
		var stack = new HorizontalStackLayout
		{
			Spacing = 5,
			VerticalOptions = LayoutOptions.Start
		};

		var openButton = new RadTemplatedButton
		{
			Content = "Open BottomSheet Content",
		};

		var closeButton = new RadTemplatedButton
		{
			Content = "Close BottomSheet Content",
		};

		openButton.Clicked += OnOpenClicked;
		closeButton.Clicked += OnCloseClicked;

		stack.Children.Add(openButton);
		stack.Children.Add(closeButton);

		this.bottomSheet.Content = stack;

		var label2 = new Label
		{
			Text = "BottomSheet Content",
			VerticalOptions = LayoutOptions.Start,
			HorizontalOptions = LayoutOptions.Center
		};

		this.bottomSheet.BottomSheetContent = label2;
		// << bottomsheet-getting-started-csharp

		this.Content = this.bottomSheet;
	}

	private void OnOpenClicked(object sender, System.EventArgs e)
	{
		this.bottomSheet.GoToBottomSheetState(BottomSheetState.PartialStateName);
	}

	private void OnCloseClicked(object sender, System.EventArgs e)
	{
		this.bottomSheet.GoToBottomSheetState(BottomSheetState.HiddenStateName);
	}
}