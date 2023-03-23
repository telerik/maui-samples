using Microsoft.Maui;
using QSF.ViewModels;
using System.Collections.Generic;
using Telerik.Maui.Controls;

namespace QSF.Examples.ProgressBarControl.ConfigurationExample;

public class ConfigurationViewModel : ExampleViewModel
{
    private string progressColor = "Default";
    private string trackColor = "Default";
    private string textColor = "Default";
    private string alternateTextColor = "White";
    private Easing animationEasing = Easing.Linear;
    private ValueDisplayMode valueDisplayMode = ValueDisplayMode.None;
    private double progressCornerRadius;
    private double trackCornerRadius;
    private double trackThickness = 4;
    private bool isIndeterminate;
    private bool canUpdateValue = true;
    private bool canUpdateValueDisplayMode = true;
    private bool canUpdateTextColor = true;
    private bool canUpdateAlternateTextColor = true;
    private bool canUpdateSegmentCount = true;
    private bool canUpdateSegmentSeparatorThickness = true;

    public ConfigurationViewModel()
    {
        this.Colors = new List<string>
        {
            "Default",
            "Black",
            "White",
            "Red",
            "Green",
            "Blue",
            "Orange"
        };

        this.Easings = new List<string>
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

        this.ValueDisplayModes = new List<ValueDisplayMode>
        {
            ValueDisplayMode.None,
            ValueDisplayMode.Percent,
            ValueDisplayMode.Value,
            ValueDisplayMode.Text
        };

#if MACCATALYST || __IOS__
        this.ProgressCornerRadius = 2;
        this.TrackCornerRadius =  2;
#elif WINDOWS
        this.ProgressCornerRadius = 1.5;
        this.TrackCornerRadius =  0.5;
        this.TrackThickness = 1;
#endif
    }

    public List<string> Colors { get; }

    public List<string> Easings { get; }

    public List<ValueDisplayMode> ValueDisplayModes { get; }

    public string TrackColor
    {
        get { return this.trackColor; }
        set { this.UpdateValue(ref this.trackColor, value); }
    }

    public string ProgressColor
    {
        get { return this.progressColor; }
        set { this.UpdateValue(ref this.progressColor, value); }
    }

    public Easing AnimationEasing
    {
        get { return this.animationEasing; }
        set { this.UpdateValue(ref this.animationEasing, value); }
    }

    public ValueDisplayMode ValueDisplayMode
    {
        get { return this.valueDisplayMode; }
        set
        {
            this.UpdateValue(ref this.valueDisplayMode, value);

            if (value == ValueDisplayMode.None)
            {
                this.CanUpdateTextColor = false;
                this.CanUpdateAlternateTextColor= false;
            }
            else if (!this.IsIndeterminate)
            {
                this.CanUpdateTextColor = true;
                this.CanUpdateAlternateTextColor= true;
            }
        }
    }

    public string TextColor
    {
        get { return this.textColor; }
        set { this.UpdateValue(ref this.textColor, value); }
    }

    public string AlternateTextColor
    {
        get { return this.alternateTextColor; }
        set { this.UpdateValue(ref this.alternateTextColor, value); }
    }

    public double ProgressCornerRadius
    {
        get { return this.progressCornerRadius; }
        set { this.UpdateValue(ref this.progressCornerRadius, value); }
    }

    public double TrackCornerRadius
    {
        get { return this.trackCornerRadius; }
        set { this.UpdateValue(ref this.trackCornerRadius, value); }
    }

    public double TrackThickness
    {
        get { return this.trackThickness; }
        set { this.UpdateValue(ref this.trackThickness, value); }
    }

    public bool IsIndeterminate
    {
        get { return this.isIndeterminate; }
        set
        {
            this.UpdateValue(ref this.isIndeterminate, value);

            this.CanUpdateValue = !value;
            this.CanUpdateValueDisplayMode = !value;
            this.CanUpdateTextColor = !value && this.ValueDisplayMode != ValueDisplayMode.None;
            this.CanUpdateAlternateTextColor= !value && this.ValueDisplayMode != ValueDisplayMode.None;
            this.CanUpdateSegmentCount = !value;
            this.CanUpdateSegmentSeparatorThickness = !value;
        }
    }

    public bool CanUpdateValue
    {
        get { return this.canUpdateValue; }
        set { this.UpdateValue(ref this.canUpdateValue, value); }
    }

    public bool CanUpdateValueDisplayMode
    {
        get { return this.canUpdateValueDisplayMode; }
        set { this.UpdateValue(ref this.canUpdateValueDisplayMode, value); }
    }

    public bool CanUpdateTextColor
    {
        get { return this.canUpdateTextColor; }
        set { this.UpdateValue(ref this.canUpdateTextColor, value); }
    }

    public bool CanUpdateAlternateTextColor
    {
        get { return this.canUpdateAlternateTextColor; }
        set { this.UpdateValue(ref this.canUpdateAlternateTextColor, value); }
    }

    public bool CanUpdateSegmentCount
    {
        get { return this.canUpdateSegmentCount; }
        set { this.UpdateValue(ref this.canUpdateSegmentCount, value); }
    }

    public bool CanUpdateSegmentSeparatorThickness
    {
        get { return this.canUpdateSegmentSeparatorThickness; }
        set { this.UpdateValue(ref this.canUpdateSegmentSeparatorThickness, value); }
    }
}
