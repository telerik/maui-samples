using System.Collections.Generic;
using System.Collections.ObjectModel;
using Telerik.Maui.Controls;

namespace QSF.Examples.SpreadStreamProcessingControl.ImportSpreadsheetExample;

public class WorkbookViewModel : NotifyPropertyChangedBase
{
    private string name;

    public WorkbookViewModel()
    {
        this.Name = "Workbook";
        this.Worksheets = new ObservableCollection<WorksheetViewModel>();
    }

    public string Name
    {
        get => this.name;
        set => this.UpdateValue(ref this.name, value);
    }

    public IList<WorksheetViewModel> Worksheets { get; }

    public override string ToString()
    {
        return this.Name;
    }
}
