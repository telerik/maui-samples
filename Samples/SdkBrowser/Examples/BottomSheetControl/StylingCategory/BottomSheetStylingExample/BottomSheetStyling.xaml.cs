using System;
using System.Linq;
using Microsoft.Maui.Controls;
using Telerik.Maui.Controls.BottomSheet;

namespace SDKBrowserMaui.Examples.BottomSheetControl.StylingCategory.BottomSheetStylingExample;

public partial class BottomSheetStyling : ContentView
{
	ViewModel vm;

	public BottomSheetStyling()
	{
		InitializeComponent();

		this.vm = new ViewModel();
		this.BindingContext = this.vm;
		this.Loaded += this.OnLoaded;
    }

    private void OnLoaded(object sender, EventArgs e)
    {
        this.peopleCollectionView.SelectedIndex = 0;
    }

    // >> bottomsheet-open-sheet-view
    private void OnSelectionChanged(object sender, Telerik.Maui.RadSelectionChangedEventArgs e)
	{
		this.bottomSheet.BottomSheetContent.BindingContext = e.AddedItems.First();
		this.bottomSheet.GoToBottomSheetState(BottomSheetState.PartialStateName);
	}
	// << bottomsheet-open-sheet-view
}