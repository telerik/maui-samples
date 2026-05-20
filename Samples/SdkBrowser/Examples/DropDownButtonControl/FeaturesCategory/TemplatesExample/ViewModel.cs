using System.Collections.ObjectModel;
using Microsoft.Maui.Graphics;
using Telerik.Maui.Controls;

namespace SDKBrowserMaui.Examples.DropDownButtonControl.FeaturesCategory.TemplatesExample;

// >> dropdownbutton-templates-viewmodel
public class ViewModel : NotifyPropertyChangedBase
{
    private ColorItem pickedColor;

    public ViewModel()
    {
        this.ItemsColors = new ObservableCollection<ColorItem>
        {
            new ColorItem { CustomColor = Color.FromArgb("#8660C5") },
            new ColorItem { CustomColor = Color.FromArgb("#EA5A51") },
            new ColorItem { CustomColor = Color.FromArgb("#2D9600") },
            new ColorItem { CustomColor = Color.FromArgb("#FFC000") },
            new ColorItem { CustomColor = Color.FromArgb("#CA1400") },
            new ColorItem { CustomColor = Color.FromArgb("#807131") },
            new ColorItem { CustomColor = Color.FromArgb("#8142BF") },
            new ColorItem { CustomColor = Color.FromArgb("#BF426E") },
            new ColorItem { CustomColor = Color.FromArgb("#FF82AE") },
            new ColorItem { CustomColor = Color.FromArgb("#C182FF") },
            new ColorItem { CustomColor = Color.FromArgb("#4B5FFA") },
            new ColorItem { CustomColor = Color.FromArgb("#FFE162") },
            new ColorItem { CustomColor = Color.FromArgb("#FF6358") },
        };
    }

    public ObservableCollection<ColorItem> ItemsColors { get; }

    public ColorItem PickedColor
    {
        get => this.pickedColor;
        set => this.UpdateValue(ref this.pickedColor, value);
    }
}

public class ColorItem
{
    public Color CustomColor { get; set; }
}
// << dropdownbutton-templates-viewmodel
