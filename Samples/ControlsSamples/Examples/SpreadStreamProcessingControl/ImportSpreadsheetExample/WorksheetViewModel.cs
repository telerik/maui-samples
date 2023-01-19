using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Dynamic;
using Telerik.Maui.Controls;

namespace QSF.Examples.SpreadStreamProcessingControl.ImportSpreadsheetExample;

public class WorksheetViewModel : NotifyPropertyChangedBase
{
    private string name;

    public WorksheetViewModel()
    {
        this.Name = "Worksheet";
        this.Columns = new ObservableCollection<ColumnViewModel>();
        this.Rows = new ObservableCollection<ExpandoObject>();
    }

    public string Name
    {
        get => this.name;
        set => this.UpdateValue(ref this.name, value);
    }

    public IList<ColumnViewModel> Columns { get; }
    public IList<ExpandoObject> Rows { get; }

    public override string ToString()
    {
        return this.Name;
    }
}
