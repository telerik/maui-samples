using Microsoft.Maui.Graphics;
using Telerik.Maui.Controls;

namespace QSF.Examples.CollectionViewControl.DragVisualTemplateExample;

public class Destination : NotifyPropertyChangedBase
{
    private string name;
    private string country;
    private string description;

    public string Name 
    {
        get => this.name;
        set => this.UpdateValue(ref this.name, value);
    }

    public string Country 
    {
        get => this.country;
        set => this.UpdateValue(ref this.country, value);
    }

    public string Description 
    {
        get => this.description;
        set => this.UpdateValue(ref this.description, value);
    }

    public string Icon { get; set; }
}
