using Microsoft.Maui.Controls;
using Microsoft.Maui.Graphics;
using QSF.ViewModels;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using Telerik.Maui.Controls.Skeleton;

namespace QSF.Examples.SkeletonControl.ConfigurationExample;

public record ColorChoice(string Name, Color Value);

public class ConfigurationViewModel : ConfigurationExampleViewModel
{
    private SkeletonType selectedSkeletonType;
    private SkeletonAnimationType selectedAnimationType;
    private Color loadingColor;
    private bool isContentLoading;
    private bool useCustomTemplate;
    private ColorChoice selectedColorChoice;
    private int repeatCount;
    private double repeatSpacing;
    private double loadingWidth;
    private double loadingHeight;

    public ConfigurationViewModel()
    {
        this.SkeletonTypes = Enum.GetValues(typeof(SkeletonType)).Cast<SkeletonType>().ToList();
        this.AnimationTypes = Enum.GetValues(typeof(SkeletonAnimationType)).Cast<SkeletonAnimationType>().ToList();

        this.ColorChoices = new ObservableCollection<ColorChoice>
        {
            new("Neutral", Colors.Gray),
            new("Neutral Light", Colors.LightGray),
            new("Slate", Colors.SlateGray),
            new("Steel", Colors.SteelBlue),
            new("Cadet", Colors.CadetBlue),
            new("Orange", Colors.Orange),
            new("Magenta", Colors.MediumVioletRed)
        };

        this.SetInitialValues();

        this.ResetCommand = new Command(this.SetInitialValues);
    }

    public IReadOnlyList<SkeletonType> SkeletonTypes { get; }

    public IReadOnlyList<SkeletonAnimationType> AnimationTypes { get; }

    public ObservableCollection<ColorChoice> ColorChoices { get; }

    public ICommand ResetCommand { get; }

    public ColorChoice SelectedColorChoice
    {
        get => this.selectedColorChoice;
        set
        {
            if (this.UpdateValue(ref this.selectedColorChoice, value) && value is not null)
            {
                this.LoadingColor = value.Value;
            }
        }
    }

    public SkeletonType SelectedSkeletonType
    {
        get => this.selectedSkeletonType;
        set => this.UpdateValue(ref this.selectedSkeletonType, value);
    }

    public SkeletonAnimationType SelectedAnimationType
    {
        get => this.selectedAnimationType;
        set => this.UpdateValue(ref this.selectedAnimationType, value);
    }

    public Color LoadingColor
    {
        get => this.loadingColor;
        set => this.UpdateValue(ref this.loadingColor, value);
    }

    public bool IsContentLoading
    {
        get => this.isContentLoading;
        set => this.UpdateValue(ref this.isContentLoading, value);
    }

    public bool UseCustomTemplate
    {
        get => this.useCustomTemplate;
        set => this.UpdateValue(ref this.useCustomTemplate, value);
    }

    public int RepeatCount
    {
        get => this.repeatCount;
        set => this.UpdateValue(ref this.repeatCount, value);
    }

    public double RepeatSpacing
    {
        get => this.repeatSpacing;
        set => this.UpdateValue(ref this.repeatSpacing, value);
    }

    public double LoadingWidth
    {
        get => this.loadingWidth;
        set => this.UpdateValue(ref this.loadingWidth, value);
    }

    public double LoadingHeight
    {
        get => this.loadingHeight;
        set => this.UpdateValue(ref this.loadingHeight, value);
    }

    private void SetInitialValues()
    {
        this.SelectedSkeletonType = SkeletonType.PersonaCircle;
        this.SelectedAnimationType = SkeletonAnimationType.Pulse;
        this.SelectedColorChoice = this.ColorChoices[0];
        this.LoadingColor = this.SelectedColorChoice.Value;
        this.IsContentLoading = true;
        this.UseCustomTemplate = false;
        this.RepeatCount = 10;
        this.RepeatSpacing = 4;
        this.LoadingWidth = 320;
        this.LoadingHeight = 96;
    }
}