using Microsoft.Maui.Graphics;
using Microsoft.Maui.Controls;
using QSF.ExampleUtilities;
using QSF.Services;
using QSF.ViewModels;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using QSF.Converters;

namespace QSF.Examples.TemplatedPickerControl.Common;

public class ColorAndSizeViewModel : ExampleViewModel
{
    private ColorModel selectedColor;
    private SizeModel selectedSize;
    private string defaultDeselectedColorKey;
    private string defaultSelectedColorKey;
    private string defaultDeselectedTextColorKey;
    private string defaultSelectedTextColorKey;
    private string highlightedValue;
    private string selectedValue;

    public ColorAndSizeViewModel()
    {
        this.defaultDeselectedColorKey = "TemplatePickerDeselectedSizeBackgroundColor";
        this.defaultSelectedColorKey = "TemplatePickerSelectedSizeBackgroundColor";
        this.defaultDeselectedTextColorKey = "DefaultTextColor";
        this.defaultSelectedTextColorKey = "TextOnAccentColor";
        this.HighlightedValue = string.Empty;
        this.SelectedValue = string.Empty;

        this.AvailableSizes = new List<SizeModel>()
        {
            new SizeModel("XS"),
            new SizeModel("S"),
            new SizeModel("M"),
            new SizeModel("L"),
            new SizeModel("XL"),
            new SizeModel("XXL")
        };

        this.AvailableColors = new List<ColorModel>()
        {
            new ColorModel("Blue", "#007AFF"),
            new ColorModel("Yellow", "#F3C163"),
            new ColorModel("Purple", "#CE3A6D"),
            new ColorModel("Orange", "#EE6C4D"),
            new ColorModel("LightGray", "#EAEAEA"),
            new ColorModel("DarkGray", "#919191")
        };

        this.SendRequestCommand = new Command(this.SendRequest, this.CanSendRequest);

        this.SelectSizeCommand = new Command<SizeModel>(
            execute: (SizeModel arg) =>
            {
                if (this.SelectedSize != null)
                {
                    this.SelectedSize.BackgroundColorKey = this.defaultDeselectedColorKey;
                    this.SelectedSize.TextColorKey = "DefaultTextColor";
                }

                arg.BackgroundColorKey = this.defaultSelectedColorKey;
                arg.TextColorKey = this.defaultSelectedTextColorKey;
                this.SelectedSize = arg;

                if (this.HighlightedValue != null && this.HighlightedValue.Contains(", "))
                {
                    this.HighlightedValue = arg.Name + this.HighlightedValue.Substring(this.HighlightedValue.IndexOf(','));
                }
                else
                {
                    this.HighlightedValue = arg.Name + ", ";
                }
            });

        this.SelectColorCommand = new Command<ColorModel>(
            execute: (ColorModel arg) =>
            {
                if (this.SelectedColor != null)
                {
                    this.SelectedColor.BorderColor = Colors.Transparent;
                }

                arg.BorderColor = arg.Color;
                this.SelectedColor = arg;
                this.HighlightedValue = this.HighlightedValue.Substring(0, this.HighlightedValue.IndexOf(',') + 2) + arg.Name;
            });

        this.AcceptCommand = new Command(Accept);
        this.CancelCommand = new Command(Cancel);

        this.SelectSizeCommand.Execute(this.AvailableSizes.First());
        this.SelectColorCommand.Execute(this.AvailableColors.First());

        this.Cards = new List<CardItem>()
        {
            new CardItem()
            {
                BackgroundColorKey = "PickerBackgroundColor1",
                BorderColorKey = "PickerBorderColor1",
                SelectedBorderColorKey = "PickerSelectedBorderColor1",
                ImageSource = "top.png"
            },
        };

        this.Accept();
    }

    public IList<CardItem> Cards { get; }

    public List<ColorModel> AvailableColors { get; private set; }

    public ColorModel SelectedColor
    {
        get => this.selectedColor;
        set => UpdateValue(ref this.selectedColor, value);
    }
    public ColorModel LastAcceptedColor { get; set; }

    public List<SizeModel> AvailableSizes { get; private set; }

    public SizeModel SelectedSize
    {
        get => this.selectedSize;
        set => UpdateValue(ref this.selectedSize, value);
    }

    public SizeModel LastAcceptedSize { get; set; }

    public ICommand SelectColorCommand { get; set; }

    public ICommand SelectSizeCommand { get; set; }

    public ICommand AcceptCommand { get; set; }

    public ICommand CancelCommand { get; set; }

    public Command SendRequestCommand { get; set; }

    public string HighlightedValue
    {
        get => this.highlightedValue;
        set => UpdateValue(ref this.highlightedValue, value);
    }
    public string SelectedValue
    {
        get => this.selectedValue;
        set
        {
            UpdateValue(ref this.selectedValue, value);
            this.SendRequestCommand?.ChangeCanExecute();
        }
    }

    private void Accept()
    {
        this.SelectedValue = this.HighlightedValue;
        this.LastAcceptedSize = this.SelectedSize;
        this.LastAcceptedColor = this.SelectedColor;
        this.SendRequestCommand.ChangeCanExecute();
    }

    private void Cancel()
    {
        if (!string.IsNullOrEmpty(this.SelectedValue))
        {
            this.HighlightedValue = this.SelectedValue;
            if (this.LastAcceptedSize != null)
            {
                this.SelectSizeCommand.Execute(this.LastAcceptedSize);
            }
            if (this.LastAcceptedColor != null)
            {
                this.SelectColorCommand.Execute(this.LastAcceptedColor);
            }
        }
        else
        {
            this.SelectSizeCommand.Execute(this.AvailableSizes.First());
            this.SelectColorCommand.Execute(this.AvailableColors.First());
        }
    }

    private void SendRequest()
    {
        var toastService = DependencyService.Get<IToastMessageService>();
        toastService.ShortAlert($"{this.SelectedValue} Women's Top Added to Basket");
        this.SelectSizeCommand.Execute(this.AvailableSizes.First());
        this.SelectColorCommand.Execute(this.AvailableColors.First());
        this.LastAcceptedColor = null;
        this.LastAcceptedSize = null;
        this.SelectedValue = null;
    }

    private bool CanSendRequest()
        => !string.IsNullOrEmpty(this.SelectedValue);
}
