using Microsoft.Maui.Controls;
using Telerik.Maui.Controls;

namespace SDKBrowserMaui.Examples.SkeletonControl.CustomDrawableCategory.CustomDrawableExample;

public partial class CustomDrawable : ContentView
{
    public CustomDrawable()
    {
        InitializeComponent();

        // >> skeleton-custom-drawable-code-behind
        // Create and configure the custom drawable
        MyCustomDrawable customDrawable = new MyCustomDrawable();
        customDrawable.SetBinding(MyCustomDrawable.FillColorProperty, 
            new Binding(nameof(RadSkeleton.AnimatedLoadingColor), source: skeleton));
        
        // Set the custom drawable as the loading view drawable
        skeleton.LoadingViewDrawable = customDrawable;
		// << skeleton-custom-drawable-code-behind
	}
}