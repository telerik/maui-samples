using System;
using System.Linq;
using Microsoft.Maui.Controls;
using Telerik.Maui.Controls.BottomSheet;

namespace SDKBrowserMaui.Examples.BottomSheetControl.FeaturesCategory.AnimationSwipeExample;

public partial class AnimationSwipe : ContentView
{
	ViewModel vm;

	public AnimationSwipe()
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

    private void OnSelectionChanged(object sender, Telerik.Maui.RadSelectionChangedEventArgs e)
	{
		this.bottomSheet.BottomSheetContent.BindingContext = e.AddedItems.First();
		this.bottomSheet.GoToBottomSheetState(BottomSheetState.PartialStateName);
	}
}