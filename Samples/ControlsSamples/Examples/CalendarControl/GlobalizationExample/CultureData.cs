namespace QSF.Examples.CalendarControl.GlobalizationExample;

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