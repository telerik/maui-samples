using QSF.ViewModels;
using QSF.Examples.DataGridControl.Common;
using System.Collections.ObjectModel;
using QSF.ExampleUtilities;

namespace QSF.Examples.DataGridControl.RowDetailsExample;

public class RowDetailsViewModel : ConfigurationExampleViewModel
{
    private bool canUserExpandMultipleRowDetails = true;
    private bool areRowDetailsFrozen;

    public RowDetailsViewModel()
    {
        this.Employees = DataGenerator.GetItems<ObservableCollection<Employee>>(ResourcePaths.EmployeesPath);
    }

    public ObservableCollection<Employee> Employees { get; private set; }

    public bool CanUserExpandMultipleRowDetails
    {
        get => this.canUserExpandMultipleRowDetails;
        set => this.UpdateValue(ref this.canUserExpandMultipleRowDetails, value);
    }

    public bool AreRowDetailsFrozen
    {
        get => this.areRowDetailsFrozen;
        set => this.UpdateValue(ref this.areRowDetailsFrozen, value);
    }
}