using System.Collections.Generic;
using Microsoft.Maui;
using Microsoft.Maui.Devices;
using QSF.ViewModels;
using Telerik.Maui;
using Telerik.Maui.Controls;
using Telerik.Maui.Controls.SlideView;

namespace QSF.Examples.SlideViewControl.ConfigurationExample;

public class ConfigurationViewModel : ExampleViewModel
{
    // SlideView Settings
    private bool hasLooping;
    private Orientation orientation = Orientation.Horizontal;
    private ButtonVisibility navigationButtonsVisibility = ButtonVisibility.HiddenWhenDisabled;
    private SlideViewInteractionMode interactionMode = SlideViewInteractionMode.Pan;
    private OverscrollMode overscrollMode = OverscrollMode.None;
    private Easing animationEasing = Easing.CubicOut;
    private double animationDuration = 400;

    // SlideViewIndicator Settings
    private int indicatorMaxVisibleItems = 5;
    private ButtonVisibility indicatorNavigationButtonsVisibility = ButtonVisibility.Collapsed;
    private double indicatorAnimationDuration = 400;
    private Easing indicatorAnimationEasing = Easing.CubicOut;

    public ConfigurationViewModel()
    {
        this.Orientations = new List<Orientation>
        {
            Orientation.Horizontal,
            Orientation.Vertical
        };

        this.NavigationButtonVisibilities = new List<ButtonVisibility>
        {
            ButtonVisibility.Visible,
            ButtonVisibility.HiddenWhenDisabled,
            ButtonVisibility.Collapsed
        };

        this.InteractionModes = new List<SlideViewInteractionMode>
        {
            SlideViewInteractionMode.None,
            SlideViewInteractionMode.Pan
        };

        this.OverscrollModes = new List<OverscrollMode>
        {
            OverscrollMode.None,
            OverscrollMode.Spring
        };

        this.AnimationEasings = new List<string>
        {
            nameof(Easing.Linear),
            nameof(Easing.SinOut),
            nameof(Easing.SinIn),
            nameof(Easing.SinInOut),
            nameof(Easing.CubicIn),
            nameof(Easing.CubicOut),
            nameof(Easing.CubicInOut),
            nameof(Easing.BounceIn),
            nameof(Easing.BounceOut),
            nameof(Easing.SpringIn),
            nameof(Easing.SpringOut)
        };

        if (DeviceInfo.Platform == DevicePlatform.MacCatalyst || DeviceInfo.Platform == DevicePlatform.WinUI)
        {
            this.InteractionMode = SlideViewInteractionMode.None;
        }
    }

    public List<Orientation> Orientations { get; }

    public List<ButtonVisibility> NavigationButtonVisibilities { get; }

    public List<SlideViewInteractionMode> InteractionModes { get; }

    public List<OverscrollMode> OverscrollModes { get; }

    public List<string> AnimationEasings { get; }

    public bool HasLooping
    {
        get { return this.hasLooping; }
        set { this.UpdateValue(ref this.hasLooping, value); }
    }

    public Orientation Orientation
    {
        get { return this.orientation; }
        set { this.UpdateValue(ref this.orientation, value); }
    }

    public ButtonVisibility NavigationButtonsVisibility
    {
        get { return this.navigationButtonsVisibility; }
        set { this.UpdateValue(ref this.navigationButtonsVisibility, value); }
    }

    public SlideViewInteractionMode InteractionMode
    {
        get { return this.interactionMode; }
        set { this.UpdateValue(ref this.interactionMode, value); }
    }

    public OverscrollMode OverscrollMode
    {
        get { return this.overscrollMode; }
        set { this.UpdateValue(ref this.overscrollMode, value); }
    }

    public Easing AnimationEasing
    {
        get { return this.animationEasing; }
        set { this.UpdateValue(ref this.animationEasing, value); }
    }

    public double AnimationDuration
    {
        get { return this.animationDuration; }
        set { this.UpdateValue(ref this.animationDuration, value); }
    }

    public int IndicatorMaxVisibleItems
    {
        get { return this.indicatorMaxVisibleItems; }
        set { this.UpdateValue(ref this.indicatorMaxVisibleItems, value); }
    }

    public ButtonVisibility IndicatorNavigationButtonsVisibility
    {
        get { return this.indicatorNavigationButtonsVisibility; }
        set { this.UpdateValue(ref this.indicatorNavigationButtonsVisibility, value); }
    }

    public Easing IndicatorAnimationEasing
    {
        get { return this.indicatorAnimationEasing; }
        set { this.UpdateValue(ref this.indicatorAnimationEasing, value); }
    }

    public double IndicatorAnimationDuration
    {
        get { return this.indicatorAnimationDuration; }
        set { this.UpdateValue(ref this.indicatorAnimationDuration, value); }
    }
}
