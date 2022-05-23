namespace SDKBrowserMaui.Examples.DataGridControl.FilteringCategory;

// >> datagrid-filtering-column
public class City
{
    public City(string name, int population)
    {
        this.Name = name;
        this.Population = population;
    }

    public string Name { get; set; }
    public int Population { get; set; }
}
// << datagrid-filtering-column