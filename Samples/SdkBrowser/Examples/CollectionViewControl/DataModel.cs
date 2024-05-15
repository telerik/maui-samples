using Telerik.Maui.Controls;

namespace SDKBrowserMaui.Examples.CollectionViewControl;

// >> collectionview-datamodel
public class DataModel : NotifyPropertyChangedBase
{
    private string continent;
    private string country;
    private string city;
    private int id;

    public string Continent
    {
        get => this.continent;
        set => this.UpdateValue(ref this.continent, value);
    }

    public string Country
    {
        get => this.country;
        set => this.UpdateValue(ref this.country, value);
    }

    public string City
    {
        get => this.city;
        set => this.UpdateValue(ref this.city, value);
    }

    public int Id
    {
        get => this.id;
        set => this.UpdateValue(ref this.id, value);
    }
}
// << collectionview-datamodel
