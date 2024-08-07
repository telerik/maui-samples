using Telerik.Maui.Controls;

namespace SDKBrowserMaui.Examples.CollectionViewControl.ItemSwipeCategory;

// >> collectionview-itemswipe-datamodel
public class DataModel : NotifyPropertyChangedBase
{
    private string country;
    private string city;
    private bool visited;

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

    public bool Visited
    {
        get => this.visited;
        set => this.UpdateValue(ref this.visited, value);
    }
}
// << collectionview-itemswipe-datamodel
