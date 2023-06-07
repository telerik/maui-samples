using QSF.Examples.DataGridControl.Common;
using QSF.ExampleUtilities;
using QSF.ViewModels;
using System.Collections.ObjectModel;

namespace QSF.Examples.DataGridControl.CustomizationExample;

public class CustomizationViewModel : ExampleViewModel
{
    public ObservableCollection<SalesPerson> People { get; private set; }

    public CustomizationViewModel()
    {
        this.People = DataGenerator.GetItems<ObservableCollection<SalesPerson>>(ResourcePaths.PeoplePath);
    }
}
