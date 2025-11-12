using QSF.ViewModels;
using Microsoft.Maui.Graphics;
using Telerik.Maui.Controls;

namespace QSF.Examples.TemplatedPickerControl.Common;

public class SizeModel : NotifyPropertyChangedBase
{
    private string name;
    private string backgroundColorKey;
    private string textColorKey;

    public SizeModel(string name, string backgroundColorKey, string textColorKey)
    {
        this.name = name;
        this.backgroundColorKey = backgroundColorKey;
        this.textColorKey = textColorKey;
    }

    // Convenience constructor with defaults
    public SizeModel(string name)
    {
        this.name = name;
        this.backgroundColorKey = "TemplatePickerDeselectedSizeBackgroundColor";
        this.textColorKey = "DefaultTextColor";
    }

    public string Name
    {
        get => this.name;
        set => UpdateValue(ref this.name, value);
    }

    public string BackgroundColorKey
    {
        get => this.backgroundColorKey;
        set => UpdateValue(ref this.backgroundColorKey, value);
    }

    public string TextColorKey
    {
        get => this.textColorKey;
        set => UpdateValue(ref this.textColorKey, value);
    }
}
