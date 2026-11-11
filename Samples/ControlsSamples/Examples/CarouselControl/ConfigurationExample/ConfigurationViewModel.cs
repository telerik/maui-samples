using System;
using System.Collections.Generic;
using QSF.ViewModels;
using Telerik.Maui;

namespace QSF.Examples.CarouselControl.ConfigurationExample;

public class ConfigurationViewModel : ConfigurationExampleViewModel
{
    private Orientation orientation = Orientation.Horizontal;
    private int highlightedIndex = 2;
    private double peekAreaRatio = 0.4;
    private double itemSpacing = 16;
    private bool allowNext = true;
    private bool allowPrevious = true;
    private bool areItemEffectsEnabled = true;

    public List<string> Items { get; } = new() { "1", "2", "3", "4", "5", "6", "7", "8", "9", "10" };

    public IReadOnlyList<Orientation> Orientations { get; } = Enum.GetValues<Orientation>();

    public int MaximumHighlightedIndex => this.Items.Count - 1;

    public Orientation Orientation
    {
        get => this.orientation;
        set => this.UpdateValue(ref this.orientation, value);
    }

    public int HighlightedIndex
    {
        get => this.highlightedIndex;
        set => this.UpdateValue(ref this.highlightedIndex, value);
    }

    public double PeekAreaRatio
    {
        get => this.peekAreaRatio;
        set => this.UpdateValue(ref this.peekAreaRatio, value);
    }

    public double ItemSpacing
    {
        get => this.itemSpacing;
        set => this.UpdateValue(ref this.itemSpacing, value);
    }

    public bool AllowNext
    {
        get => this.allowNext;
        set => this.UpdateValue(ref this.allowNext, value);
    }

    public bool AllowPrevious
    {
        get => this.allowPrevious;
        set => this.UpdateValue(ref this.allowPrevious, value);
    }

    public bool AreItemEffectsEnabled
    {
        get => this.areItemEffectsEnabled;
        set => this.UpdateValue(ref this.areItemEffectsEnabled, value);
    }

}
