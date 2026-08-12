using QSF.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using Telerik.Maui;
using Telerik.Maui.Controls.CircularSlider;
using Telerik.Maui.Controls.RangeSlider;
using Telerik.Maui.Controls.Sliders;

namespace QSF.Examples.CircularSliderControl.ConfigurationExample;

public class ConfigurationViewModel : ExampleViewModel
{
    private const double MinRadiusFactor = 0.1;
    private const double MaxRadiusFactor = 1.0;
    private double tickStep;
    private double tickThickness;
    private double tickLength;
    private double labelStep;
    private double backTrackThickness = 4;
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
    private double startAngle = 225;
    private double sweepAngle = 270;
    private SweepDirection sweepDirection = SweepDirection.Clockwise;
    private double radiusFactor = 0.7;

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
#elif MACCATALYST
        this.TickThickness = 2;
        this.TickLength = 6;
#elif IOS
        this.TickThickness = 4;
        this.TickLength = 10;
#elif WINDOWS
        this.TickThickness = 1;
        this.TickLength = 4;
#endif
    }

    public List<string> Colors { get; }

    public IEnumerable<SliderTicksPlacement> TicksPlacements { get; } = Enum.GetValues(typeof(SliderTicksPlacement)).Cast<SliderTicksPlacement>();

    public IEnumerable<SliderLabelsPlacement> LabelsPlacements { get; } = Enum.GetValues(typeof(SliderLabelsPlacement)).Cast<SliderLabelsPlacement>();

    public IEnumerable<SliderSnapMode> SnapModes { get; } = Enum.GetValues(typeof(SliderSnapMode)).Cast<SliderSnapMode>();

    public IEnumerable<SliderDragMode> DragModes { get; } = Enum.GetValues(typeof(SliderDragMode)).Cast<SliderDragMode>();

    public IEnumerable<SweepDirection> SweepDirections { get; } = Enum.GetValues(typeof(SweepDirection)).Cast<SweepDirection>();

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
        set
        {
            if (this.UpdateValue(ref this.backTrackThickness, value))
            {
                this.OnPropertyChanged(nameof(this.AppliedRadiusFactor));
            }
        }
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

    public double StartAngle
    {
        get => this.startAngle;
        set => this.UpdateValue(ref this.startAngle, value);
    }

    public double SweepAngle
    {
        get => this.sweepAngle;
        set => this.UpdateValue(ref this.sweepAngle, value);
    }

    public SweepDirection SweepDirection
    {
        get => this.sweepDirection;
        set => this.UpdateValue(ref this.sweepDirection, value);
    }

    public double RadiusFactor
    {
        get => this.radiusFactor;
        set
        {
            if (this.UpdateValue(ref this.radiusFactor, value))
            {
                this.OnPropertyChanged(nameof(this.AppliedRadiusFactor));
            }
        }
    }

    public double AppliedRadiusFactor
    {
        get
        {
            double requested = Math.Clamp(this.radiusFactor, MinRadiusFactor, MaxRadiusFactor);
            return requested;
        }
    }
}
