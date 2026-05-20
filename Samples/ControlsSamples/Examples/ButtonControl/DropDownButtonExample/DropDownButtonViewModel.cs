using QSF.Examples.ButtonControl.FirstLookExample;
using QSF.ViewModels;
using System.Collections.ObjectModel;
using Telerik.Maui.Controls;
using Telerik.Maui.Controls.DropDownButton;

namespace QSF.Examples.ButtonControl.DropDownButtonExample;

public class DropDownButtonViewModel : ExampleViewModel
{
    private bool isDropDownOpen = false;
    private bool isDropDownIndicatorVisible = true;
    private DropDownButtonIndicatorPosition dropDownIndicatorPosition = DropDownButtonIndicatorPosition.Right;
    private PlacementMode dropDownPlacement = PlacementMode.Bottom;
    private TimeSpan autoOpenDelay = TimeSpan.Zero;
    private PopupAnimationType dropDownAnimation = PopupAnimationType.Slide;
    private string customContent = "Wi-Fi Networks";

    public DropDownButtonViewModel()
    {
        this.WiFiNetworks = new ObservableCollection<MyData>
        {
            new MyData { Name = "Home Wi-Fi", Icon = "\ue85b" },
            new MyData { Name = "Mobile Hotspot", Icon = "\ue85d" },
            new MyData { Name = "Office Wi-Fi", Icon = "\ue8a3" },
            new MyData { Name = "Public Wi-Fi", Icon = "\ue804" },
        };
    }

    public ObservableCollection<MyData> WiFiNetworks { get; }

    public bool IsDropDownOpen
    {
        get => this.isDropDownOpen;
        set => this.UpdateValue(ref this.isDropDownOpen, value);
    }

    public string CustomContent
    {
        get => this.customContent;
        set => this.UpdateValue(ref this.customContent, value);
    }

    public bool IsDropDownIndicatorVisible
    {
        get => this.isDropDownIndicatorVisible;
        set => this.UpdateValue(ref this.isDropDownIndicatorVisible, value);
    }

    public DropDownButtonIndicatorPosition DropDownIndicatorPosition
    {
        get => this.dropDownIndicatorPosition;
        set => this.UpdateValue(ref this.dropDownIndicatorPosition, value);
    }

    public PlacementMode DropDownPlacement
    {
        get => this.dropDownPlacement;
        set => this.UpdateValue(ref this.dropDownPlacement, value);
    }

    public TimeSpan AutoOpenDelay
    {
        get => this.autoOpenDelay;
        set => this.UpdateValue(ref this.autoOpenDelay, value);
    }

    public PopupAnimationType DropDownAnimation
    {
        get => this.dropDownAnimation;
        set => this.UpdateValue(ref this.dropDownAnimation, value);
    }
}
