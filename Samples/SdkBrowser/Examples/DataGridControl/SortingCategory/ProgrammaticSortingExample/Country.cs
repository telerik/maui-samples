namespace SDKBrowserMaui.Examples.DataGridControl.SortingCategory.ProgrammaticSortingExample;

public class Country
{
    public Country(string name, double population)
    {
        this.Name = name;
        this.Population = population;
    }

    public string Name { get; set; }
    public double Population { get; set; }
}