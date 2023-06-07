namespace QSF.Examples.CalendarControl.ConfigurationExample;

public class CultureData
{
    public CultureData(string displayName, string name)
    {
        this.Name = name;
        this.DisplayName = displayName;
    }

    public string Name { get; set; }

    public string DisplayName { get; set; }
}