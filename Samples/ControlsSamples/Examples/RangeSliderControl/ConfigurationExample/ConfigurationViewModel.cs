using QSF.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using Telerik.Maui.Controls.RangeSlider;

namespace QSF.Examples.RangeSliderControl.ConfigurationExample;

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
    private string startThumbFill = "Default";
    private string rangeTrackFill = "Default";
    private string endThumbFill = "Default";
    private SliderTicksPlacement ticksPlacement;
    private SliderLabelsPlacement labelsPlacement;
    private SliderSnapMode snapMode;
    private RangeSliderDragMode dragMode = RangeSliderDragMode.StartThumb | RangeSliderDragMode.RangeTrack | RangeSliderDragMode.EndThumb;
    private bool canDragStartThumb = true;
    private bool canDragRangeTrack = true;
    private bool canDragEndThumb = true;
    private bool isDragStartThumbEnabled = true;
    private bool isDragRangeTrackEnabled = true;
    private bool isDragEndThumbEnabled = true;
    private bool isDragDisabled = false;

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

    public IEnumerable<RangeSliderDragMode> DragModes { get; } = Enum.GetValues(typeof(RangeSliderDragMode)).Cast<RangeSliderDragMode>();

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

    public string StartThumbFill
    {
        get => this.startThumbFill;
        set => this.UpdateValue(ref this.startThumbFill, value);
    }

    public string RangeTrackFill
    {
        get => this.rangeTrackFill;
        set => this.UpdateValue(ref this.rangeTrackFill, value);
    }

    public string EndThumbFill
    {
        get => this.endThumbFill;
        set => this.UpdateValue(ref this.endThumbFill, value);
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

    public RangeSliderDragMode DragMode
    {
        get => this.dragMode;
        set => this.UpdateValue(ref this.dragMode, value);
    }

    public bool CanDragStartThumb
    {
        get => this.canDragStartThumb;
        set
        {
            if (this.UpdateValue(ref this.canDragStartThumb, value))
            {
                this.UpdateDragMode();
            }
        }
    }

    public bool CanDragRangeTrack
    {
        get => this.canDragRangeTrack;
        set
        {
            if (this.UpdateValue(ref this.canDragRangeTrack, value))
            {
                this.UpdateDragMode();
            }
        }
    }

    public bool CanDragEndThumb
    {
        get => this.canDragEndThumb;
        set
        {
            if (this.UpdateValue(ref this.canDragEndThumb, value))
            {
                this.UpdateDragMode();
            }
        }
    }

    public bool IsDragStartThumbEnabled
    {
        get => this.isDragStartThumbEnabled;
        set => this.UpdateValue(ref this.isDragStartThumbEnabled, value);
    }

    public bool IsDragRangeTrackEnabled
    {
        get => this.isDragRangeTrackEnabled;
        set => this.UpdateValue(ref this.isDragRangeTrackEnabled, value);
    }


    public bool IsDragEndThumbEnabled
    {
        get => this.isDragEndThumbEnabled;
        set => this.UpdateValue(ref this.isDragEndThumbEnabled, value);
    }

    public bool IsDragDisabled
    {
        get => this.isDragDisabled;
        set
        {
            if (this.UpdateValue(ref this.isDragDisabled, value))
            {
                this.UpdateDragMode();

                this.IsDragStartThumbEnabled = !value;
                this.IsDragEndThumbEnabled = !value;
                this.IsDragRangeTrackEnabled = !value;
            }
        }
    }

    private void UpdateDragMode()
    {
        if (this.IsDragDisabled)
        {
            this.DragMode = RangeSliderDragMode.Disabled;
        }
        else
        {
            var mode = RangeSliderDragMode.Disabled;
            if (this.canDragStartThumb)
            {
                mode |= RangeSliderDragMode.StartThumb;
            }

            if (this.canDragRangeTrack)
            {
                mode |= RangeSliderDragMode.RangeTrack;
            }

            if (this.canDragEndThumb)
            {
                mode |= RangeSliderDragMode.EndThumb;
            }

            this.DragMode = mode;
        }
    }
}
