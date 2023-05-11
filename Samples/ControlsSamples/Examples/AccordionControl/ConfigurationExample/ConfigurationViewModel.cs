using Microsoft.Maui;
using QSF.ViewModels;
using System.Collections.ObjectModel;

namespace QSF.Examples.AccordionControl.ConfigurationExample;

public class ConfigurationViewModel : ConfigurationExampleViewModel
{
    private Easing animationEasing = Easing.Linear;
    private double animationDuration = 500;
    private double spacing;
    private bool canCollapseAllItems;
    private bool canExpandMultipleItems;
    private bool isAnimationEnabled = true;

    public ObservableCollection<string> AnimationEasings { get; } = new ObservableCollection<string>()
        {
            "Linear", "SinOut", "SinIn", "SinInOut", "CubicIn", "CubicOut", "CubicInOut", "BounceOut", "BounceIn", "SpringIn", "SpringOut"
        };

    public bool IsAnimationEnabled
    {
        get => this.isAnimationEnabled;
        set => this.UpdateValue(ref this.isAnimationEnabled, value);
    }

    public bool CanExpandMultipleItems
    {
        get => this.canExpandMultipleItems;
        set => this.UpdateValue(ref this.canExpandMultipleItems, value);
    }

    public bool CanCollapseAllItems
    {
        get => this.canCollapseAllItems;
        set => this.UpdateValue(ref this.canCollapseAllItems, value);
    }

    public double Spacing
    {
        get => this.spacing;
        set => this.UpdateValue(ref this.spacing, value);
    }

    public double AnimationDuration
    {
        get => this.animationDuration;
        set => this.UpdateValue(ref this.animationDuration, value);
    }

    public Easing AnimationEasing
    {
        get => this.animationEasing;
        set => this.UpdateValue(ref this.animationEasing, value);
    }
}
