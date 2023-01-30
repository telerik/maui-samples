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
    private bool isVisible;

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
        set { this.UpdateValue(ref this.valueDisplayMode, value); }
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

    public bool IsVisible
    {
        get { return this.isVisible; }
        set { this.UpdateValue(ref this.isVisible, value); }
    }
}
