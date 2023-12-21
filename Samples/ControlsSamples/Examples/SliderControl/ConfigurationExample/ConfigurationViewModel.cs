using QSF.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using Telerik.Maui.Controls.RangeSlider;
using Telerik.Maui.Controls.Slider;

namespace QSF.Examples.SliderControl.ConfigurationExample;

public class ConfigurationViewModel : ExampleViewModel
{
    private double tickStep;
    private double tickThickness;
    private double tickLength;
    private double labelStep;
    private double backTrackThickness = 4;
    private double backTrackExtent;
    private string inRangeTickColor = "Default";
    private string outOfRangeTickColor = "Default";
    private string textColor = "Default";
    private string backTrackColor = "Default";
    private string thumbFill = "Default";
    private string rangeTrackFill = "Default";
    private SliderTicksPlacement ticksPlacement;
    private SliderLabelsPlacement labelsPlacement;
    private SliderSnapMode snapMode;
    private SliderDragMode dragMode = SliderDragMode.Free;

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

#if ANDROID
        this.TickThickness = 2;
        this.TickLength = 2;
        this.BackTrackExtent = 3;
#elif MACCATALYST
        this.TickThickness = 2;
        this.TickLength = 6;
#elif IOS
        this.TickThickness = 4;
        this.TickLength = 10;
#elif WINDOWS
        this.TickThickness = 1;
        this.TickLength = 4;
        this.BackTrackExtent = 4;
#endif
    }

    public List<string> Colors { get; }

    public IEnumerable<SliderTicksPlacement> TicksPlacements { get; } = Enum.GetValues(typeof(SliderTicksPlacement)).Cast<SliderTicksPlacement>();

    public IEnumerable<SliderLabelsPlacement> LabelsPlacements { get; } = Enum.GetValues(typeof(SliderLabelsPlacement)).Cast<SliderLabelsPlacement>();

    public IEnumerable<SliderSnapMode> SnapModes { get; } = Enum.GetValues(typeof(SliderSnapMode)).Cast<SliderSnapMode>();

    public IEnumerable<SliderDragMode> DragModes { get; } = Enum.GetValues(typeof(SliderDragMode)).Cast<SliderDragMode>();

    public double TickStep
    {
        get => this.tickStep;
        set => this.UpdateValue(ref this.tickStep, value);
    }

    public double TickThickness
    {
        get => this.tickThickness;
        set => this.UpdateValue(ref this.tickThickness, value);
    }

    public double TickLength
    {
        get => this.tickLength;
        set => this.UpdateValue(ref this.tickLength, value);
    }

    public double LabelStep
    {
        get => this.labelStep;
        set => this.UpdateValue(ref this.labelStep, value);
    }

    public double BackTrackThickness
    {
        get => this.backTrackThickness;
        set => this.UpdateValue(ref this.backTrackThickness, value);
    }

    public double BackTrackExtent
    {
        get => this.backTrackExtent;
        set => this.UpdateValue(ref this.backTrackExtent, value);
    }

    public string InRangeTickColor
    {
        get => this.inRangeTickColor;
        set => this.UpdateValue(ref this.inRangeTickColor, value);
    }

    public string OutOfRangeTickColor
    {
        get => this.outOfRangeTickColor;
        set => this.UpdateValue(ref this.outOfRangeTickColor, value);
    }

    public string TextColor
    {
        get => this.textColor;
        set => this.UpdateValue(ref this.textColor, value);
    }

    public string BackTrackColor
    {
        get => this.backTrackColor;
        set => this.UpdateValue(ref this.backTrackColor, value);
    }

    public string ThumbFill
    {
        get => this.thumbFill;
        set => this.UpdateValue(ref this.thumbFill, value);
    }

    public string RangeTrackFill
    {
        get => this.rangeTrackFill;
        set => this.UpdateValue(ref this.rangeTrackFill, value);
    }

    public SliderTicksPlacement TicksPlacement
    {
        get => this.ticksPlacement;
        set => this.UpdateValue(ref this.ticksPlacement, value);
    }

    public SliderLabelsPlacement LabelsPlacement
    {
        get => this.labelsPlacement;
        set => this.UpdateValue(ref this.labelsPlacement, value);
    }

    public SliderSnapMode SnapMode
    {
        get => this.snapMode;
        set => this.UpdateValue(ref this.snapMode, value);
    }

    public SliderDragMode DragMode
    {
        get => this.dragMode;
        set => this.UpdateValue(ref this.dragMode, value);
    }
}
