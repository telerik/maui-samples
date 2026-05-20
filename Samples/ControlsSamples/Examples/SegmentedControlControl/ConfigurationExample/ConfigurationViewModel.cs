using Microsoft.Maui;
using Microsoft.Maui.Controls;
using QSF.ViewModels;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Telerik.Maui.Controls.SegmentedControl;

namespace QSF.Examples.SegmentedControlControl.ConfigurationExample;

public class ConfigurationViewModel : ExampleViewModel
{
    private SegmentedControlSizeMode selectedSizeMode = SegmentedControlSizeMode.Star;
    private double selectedWidthRequest = -1;
    private double selectedFontSize = 14;
    private FontAttributes selectedFontAttributes = FontAttributes.None;
    private TextDecorations selectedTextDecorations = TextDecorations.None;
    private TextAlignment selectedHorizontalAlignment = TextAlignment.Center;
    private string textColor = null;
    private string selectedTextColor = null;
    private string selectedSegmentedControlBorderColor;
    private string selectedSizeModeOption;

    public ConfigurationViewModel()
    {
        this.CategoryItems = new ObservableCollection<CategoryItem>
        {
            new CategoryItem { Name = "Dinner", Category = "Food" },
            new CategoryItem { Name = "Drinks", Category = "Beverages" },
            new CategoryItem { Name = "Desserts", Category = "Sweets" },
        };

        this.SelectedSizeModeOption = this.SizeModes[0];
    }

    public ObservableCollection<CategoryItem> CategoryItems { get; }

    public IReadOnlyList<string> SizeModes { get; } = new string[] { "Star", "Auto", "Fixed (80 px)" };

    public IReadOnlyList<FontAttributes> FontAttributeOptions { get; } = Enum.GetValues<FontAttributes>();

    public IReadOnlyList<TextDecorations> TextDecorationOptions { get; } = Enum.GetValues<TextDecorations>();

    public IReadOnlyList<TextAlignment> AlignmentOptions { get; } = Enum.GetValues<TextAlignment>();

    public IReadOnlyList<double> FontSizeOptions { get; } =
        new double[] { 10, 12, 14, 16, 18, 20, 24 };

    public IReadOnlyList<string> ColorOptions { get; } =
        new[] { "Default", "Black", "White", "Gray", "Blue", "Red", "Green", "Orange", "Purple" };

    public IReadOnlyList<double> SegmentedControlThickness { get; } =
        new double[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };

    public SegmentedControlSizeMode SelectedSizeMode
    {
        get => this.selectedSizeMode;
        set
        {
            if (this.UpdateValue<SegmentedControlSizeMode>(ref this.selectedSizeMode, value))
            {
                this.SelectedWidthRequest = this.selectedSizeMode != SegmentedControlSizeMode.Fixed ? -1 : 80;
            }
        }
    }

     public double SelectedWidthRequest
    {
        get => this.selectedWidthRequest;
        set => this.UpdateValue<double>(ref this.selectedWidthRequest, value);
    }

    public double SelectedFontSize
    {
        get => this.selectedFontSize;
        set => this.UpdateValue<double>(ref this.selectedFontSize, value);
    }

    public FontAttributes SelectedFontAttributes
    {
        get => this.selectedFontAttributes;
        set => this.UpdateValue<FontAttributes>(ref this.selectedFontAttributes, value);
    }

    public TextDecorations SelectedTextDecorations
    {
        get => this.selectedTextDecorations;
        set => this.UpdateValue<TextDecorations>(ref this.selectedTextDecorations, value);
    }

    public TextAlignment SelectedHorizontalAlignment
    {
        get => this.selectedHorizontalAlignment;
        set => this.UpdateValue<TextAlignment>(ref this.selectedHorizontalAlignment, value);
    }

    public string TextColor
    {
        get => this.textColor != null ? this.textColor : this.ColorOptions[0];
        set => this.UpdateValue<string>(ref this.textColor, value);
    }

    public string SelectedTextColor
    {
        get => this.selectedTextColor != null ? this.selectedTextColor : this.ColorOptions[0];
        set => this.UpdateValue<string>(ref this.selectedTextColor, value);
    }

    public string SelectedSegmentedControlBorderColor
    {
        get => this.selectedSegmentedControlBorderColor != null ? this.selectedSegmentedControlBorderColor : this.ColorOptions[0];
        set => this.UpdateValue<string>(ref this.selectedSegmentedControlBorderColor, value);
    }

    public string SelectedSizeModeOption
    {
        get => this.selectedSizeModeOption;
        set
        {
            if (this.UpdateValue<string>(ref this.selectedSizeModeOption, value))
            {
                this.SelectedSizeMode = value switch
                {
                    "Star" => SegmentedControlSizeMode.Star,
                    "Auto" => SegmentedControlSizeMode.Auto,
                    "Fixed (80 px)" => SegmentedControlSizeMode.Fixed,
                    _ => SegmentedControlSizeMode.Star
                };
            }
        }
    }
}
