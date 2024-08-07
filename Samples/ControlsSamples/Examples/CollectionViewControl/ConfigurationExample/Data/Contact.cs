using Telerik.Maui.Controls;

namespace QSF.Examples.CollectionViewControl.ConfigurationExample;

public class Contact : NotifyPropertyChangedBase
{
    private string name;
    private bool isFavorite;

    public string Name
    {
        get => this.name;
        set => this.UpdateValue(ref this.name, value);
    }

    public bool IsFavorite
    {
        get => this.isFavorite; 
        set => this.UpdateValue(ref this.isFavorite, value);
    }
}