using Telerik.Maui.Controls;

namespace QSF.Examples.SpreadStreamProcessingControl.ImportSpreadsheetExample;

public class ColumnViewModel : NotifyPropertyChangedBase
{
    private string name;

    public ColumnViewModel()
    {
        this.Name = "Column";
    }

    public string Name
    {
        get => this.name;
        set => this.UpdateValue(ref this.name, value);
    }

    public override string ToString()
    {
        return this.Name;
    }
}
