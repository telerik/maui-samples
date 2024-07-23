using System;
using System.Linq;
using Microsoft.Maui.Controls;
using Telerik.Maui.Controls.Compatibility.DataControls;

namespace QSF.Pages;

public class QSFPage : ContentPage
{
    public virtual Grid SafeAreaGridWithHeader => null;

	public virtual View FullScreenScrollView => null;

	public virtual View Acrylic => null;

	public virtual Grid ContentGrid => null;

#if ANDROID
	private bool safeAreaSet = false;
	private bool scrollViewPaddingSet = false;
#endif

	protected virtual void BaseInitializeComponent()
	{
		if (this.FullScreenScrollView is RadListView radListView)
		{
			// radListView.Scrolled += this.FullScreenScrollViewScrolled;
		}
		else if (this.FullScreenScrollView is ScrollView scrollView)
		{
			scrollView.Scrolled += this.FullScreenScrollViewScrolled;
		}

		this.SetSafeAreaGridLayout();

		if (this.ContentGrid != null)
		{
			// if (this.FullScreenScrollView == null)
			// {
				Grid.SetRow(this.ContentGrid, 2);
				Grid.SetRowSpan(this.ContentGrid, 1);
				Grid.SetColumn(this.ContentGrid, 1);
				Grid.SetColumnSpan(this.ContentGrid, 1);
			// }
			// else
			// {
			// 	Grid.SetRow(this.ContentGrid, 0);
			// 	Grid.SetRowSpan(this.ContentGrid, 4);
			// 	Grid.SetColumn(this.ContentGrid, 0);
			// 	Grid.SetColumnSpan(this.ContentGrid, 3);
			// }
		}

		this.SetFullScreenListViewPaddings();
	}

    protected override void OnSizeAllocated(double width, double height)
    {
		this.SetSafeAreaGridLayout();

		base.OnSizeAllocated(width, height);

		this.SetFullScreenListViewPaddings();
    }

	protected virtual void FullScreenScrollViewScrolled(object sender, ScrolledEventArgs e)
    {
        if (this.Acrylic != null)
        {
            var t = Math.Clamp((e.ScrollY - 20) * (1.0 / 80.0), 0, 1);
            var tEased = 0.5 - 0.5 * Math.Cos(t * Math.PI);
			var acrylicOpacity = 1 - tEased * 0.85;
            // this.Acrylic.TintOpacity = acrylicOpacity;
        }
    }

	protected virtual void SetSafeAreaGridLayout()
	{
		if (this.SafeAreaGridWithHeader != null)
		{
#if IOS
			var insets = System.OperatingSystem.IsIOSVersionAtLeast(13, 0)
				? UIKit.UIApplication.SharedApplication?.ConnectedScenes?.OfType<UIKit.UIWindowScene>()?.FirstOrDefault()?.Windows?.FirstOrDefault()?.SafeAreaInsets
				: UIKit.UIApplication.SharedApplication?.Windows?.FirstOrDefault()?.SafeAreaInsets;

			if (insets != null)
			{
				this.SafeAreaGridWithHeader.RowDefinitions[0].Height = insets.Value.Top.Value;
				this.SafeAreaGridWithHeader.RowDefinitions[3].Height = insets.Value.Bottom.Value;
				this.SafeAreaGridWithHeader.ColumnDefinitions[0].Width = insets.Value.Left.Value;
				this.SafeAreaGridWithHeader.ColumnDefinitions[2].Width = insets.Value.Right.Value;
			}
#elif ANDROID
			if (!this.safeAreaSet)
			{
				var insets = Microsoft.Maui.ApplicationModel.Platform.CurrentActivity?.Window?.DecorView?.RootView?.RootWindowInsets;
				if (insets != null) {
					var density = Microsoft.Maui.Devices.DeviceDisplay.MainDisplayInfo.Density;
					this.SafeAreaGridWithHeader.RowDefinitions[0].Height = insets.StableInsetTop / density;
					this.SafeAreaGridWithHeader.RowDefinitions[3].Height = insets.StableInsetBottom / density;
					this.SafeAreaGridWithHeader.ColumnDefinitions[0].Width = insets.StableInsetLeft / density;
					this.SafeAreaGridWithHeader.ColumnDefinitions[2].Width = insets.StableInsetRight / density;

					this.safeAreaSet = true;
				}
			}
#endif	
		}
	}

	protected virtual void SetFullScreenListViewPaddings()
	{
		if (this.FullScreenScrollView is RadListView radListView)
		{
#if IOS
			if (radListView?.Handler?.PlatformView is TelerikUI.TKListView platformList)
			{
				// platformList.ContentInset = new UIKit.UIEdgeInsets(50, 0, 0, 0);
				// Re-implement in CollectionView instead of fixing up the native RadListView
				// platformList.VerticalScrollIndicatorInsets = new UIKit.UIEdgeInsets(50, 0, 0, 0);
			}
#elif ANDROID
			if (!this.scrollViewPaddingSet && radListView?.Handler?.PlatformView is Telerik.Maui.Controls.Compatibility.DataControlsRenderer.Android.ListView.RadListViewWrapper platformList)
			{
				// var density = Microsoft.Maui.Devices.DeviceDisplay.MainDisplayInfo.Density;
				// var decor = Microsoft.Maui.ApplicationModel.Platform.CurrentActivity.Window.DecorView;
				// var insets = decor.RootView.RootWindowInsets;

				// platformList.ListView.SetClipToPadding(false);
				// platformList.ListView.SetPadding(insets.StableInsetLeft, insets.StableInsetTop + (int)(50.0 * density), insets.StableInsetRight, insets.StableInsetBottom);
				// platformList.ListView.ScrollToStart();

				// this.scrollViewPaddingSet = true;
			}
#endif
		}
		else if (this.FullScreenScrollView is ScrollView scrollview)
		{
#if IOS
			if (scrollview?.Handler?.PlatformView is UIKit.UIScrollView platformView)
			{
				// platformView.ContentInset = new UIKit.UIEdgeInsets(50, 0, 0, 0);
				// platformView.VerticalScrollIndicatorInsets = new UIKit.UIEdgeInsets(50, 0, 0, 0);
			}
#elif ANDROID
			if (!scrollViewPaddingSet && scrollview?.Handler?.PlatformView is AndroidX.Core.Widget.NestedScrollView platformView)
			{
				// var density = Microsoft.Maui.Devices.DeviceDisplay.MainDisplayInfo.Density;
				// var decor = Microsoft.Maui.ApplicationModel.Platform.CurrentActivity.Window.DecorView;
				// var insets = decor.RootView.RootWindowInsets;

				// platformView.SetClipToPadding(false);
				// platformView.SetPadding(insets.StableInsetLeft, insets.StableInsetTop + (int)(50.0 * density), insets.StableInsetRight, insets.StableInsetBottom);

				// this.scrollViewPaddingSet = true;
			}
#endif
		}
	}
}