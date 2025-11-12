using Microsoft.Maui.Controls;
using QSF.ExampleUtilities;
using QSF.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using Telerik.Maui.Controls;
using Telerik.Maui.Controls.Compatibility.DataControls;
using Telerik.Maui.Controls.DataControls;

namespace QSF.Views;

public partial class QSFPageContentView : RadContentView
{
    private Grid SafeAreaGridWithHeader;

	private View FullScreenScrollView;

	private Grid BodyGrid;

	private View Acrylic;

#if ANDROID
	private bool safeAreaSet = false;
	private bool scrollViewPaddingSet = false;
#endif

    public static readonly BindableProperty HeaderControlTemplateProperty =
        BindableProperty.Create(nameof(HeaderControlTemplate), typeof(ControlTemplate), typeof(QSFPageContentView), null);

    public static readonly BindableProperty BodyControlTemplateProperty =
        BindableProperty.Create(nameof(BodyControlTemplate), typeof(ControlTemplate), typeof(QSFPageContentView), null);


    public QSFPageContentView()
    {
        this.InitializeComponent();
        this.BaseInitializeComponent();
    }

    public ControlTemplate HeaderControlTemplate
    {
        get => (ControlTemplate)this.GetValue(HeaderControlTemplateProperty);
        set => this.SetValue(HeaderControlTemplateProperty, value);
    }

    public ControlTemplate BodyControlTemplate
    {
        get => (ControlTemplate)this.GetValue(BodyControlTemplateProperty);
        set => this.SetValue(BodyControlTemplateProperty, value);
    }

    protected override void OnApplyTemplate()
    {
        base.OnApplyTemplate();

        this.SafeAreaGridWithHeader = this.GetTemplateChild("PART_Root") as Grid;
        this.BodyGrid = this.GetTemplateChild("PART_Body") as Grid;

#if IOS
        this.Acrylic = this.GetTemplateChild("PART_Acrylic") as Grid;
#endif
    }

    protected override void OnSizeAllocated(double width, double height)
    {
        this.SetSafeAreaGridLayout();

        base.OnSizeAllocated(width, height);

        this.SetFullScreenListViewPaddings();
    }

    private void BaseInitializeComponent()
    {
        // TODO: First get the scrollable view and assign it to FullScreenScrollView in OnApplyTemplate.
        // Then update this to be against RadCollectionView.
        if (this.FullScreenScrollView is RadListView radListView)
        {
            // radListView.Scrolled += this.FullScreenScrollViewScrolled;
        }
        else if (this.FullScreenScrollView is ScrollView scrollView)
        {
            scrollView.Scrolled += this.FullScreenScrollViewScrolled;
        }

        this.SetSafeAreaGridLayout();

        if (this.BodyGrid != null)
        {
            Grid.SetRow(this.BodyGrid, 2);
            Grid.SetRowSpan(this.BodyGrid, 1);
            Grid.SetColumn(this.BodyGrid, 1);
            Grid.SetColumnSpan(this.BodyGrid, 1);
        }

        this.SetFullScreenListViewPaddings();
    }

    private void SetSafeAreaGridLayout()
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

                ((Microsoft.Maui.IView)this.SafeAreaGridWithHeader).InvalidateMeasure();
            }
#elif ANDROID
            if (!this.safeAreaSet)
            {
                var insets = Microsoft.Maui.ApplicationModel.Platform.CurrentActivity?.Window?.DecorView?.RootView?.RootWindowInsets;
                if (insets != null) {
                    var density = Microsoft.Maui.Devices.DeviceDisplay.MainDisplayInfo.Density;
                    this.SafeAreaGridWithHeader.RowDefinitions[0].Height = 0;
                    this.SafeAreaGridWithHeader.RowDefinitions[3].Height = 0;
                    this.SafeAreaGridWithHeader.ColumnDefinitions[0].Width = insets.StableInsetLeft / density;
                    this.SafeAreaGridWithHeader.ColumnDefinitions[2].Width = insets.StableInsetRight / density;

                    this.safeAreaSet = true;
                }
            }
#endif	
        }
    }

    private void SetFullScreenListViewPaddings()
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

    private void FullScreenScrollViewScrolled(object sender, ScrolledEventArgs e)
    {
        if (this.Acrylic != null)
        {
            var t = Math.Clamp((e.ScrollY - 20) * (1.0 / 80.0), 0, 1);
            var tEased = 0.5 - 0.5 * Math.Cos(t * Math.PI);
            var acrylicOpacity = 1 - tEased * 0.85;
            // this.Acrylic.TintOpacity = acrylicOpacity;
        }
    }
}
