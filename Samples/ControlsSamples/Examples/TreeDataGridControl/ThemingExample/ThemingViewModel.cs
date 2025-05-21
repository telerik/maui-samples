using QSF.Examples.TreeDataGridControl.Common;
using QSF.ExampleUtilities;
using QSF.ViewModels;
using System.Collections.ObjectModel;
using System.Xml.Serialization;

namespace QSF.Examples.TreeDataGridControl.ThemingExample;

public class ThemingViewModel : ExampleViewModel
{
    private ObservableCollection<EmployeeViewModel> employees;

    public ThemingViewModel()
    {
        this.Employees = DataGenerator.GetItems<ObservableCollection<EmployeeViewModel>>(DataSourcePaths.EmployeeDataSource);
    }

    [XmlArray("Employees")]
    [XmlArrayItem("Employee")]
    public ObservableCollection<EmployeeViewModel> Employees
    {
        get { return this.employees; }
        set { this.UpdateValue(ref this.employees, value); }
    }
}
