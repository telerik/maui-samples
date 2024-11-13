using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using QSF.Examples.DataGridControl.Common;
using QSF.ExampleUtilities;
using QSF.ViewModels;

namespace QSF.Examples.ExpanderControl.ThemingExample;

public class ThemingViewModel : ExampleViewModel
{
    public IEnumerable<SalesPerson> SalesReps { get; private set; }
    public ThemingViewModel()
    {
        SalesReps = DataGenerator.GetItems<ObservableCollection<SalesPerson>>(ResourcePaths.PeoplePath).Take(20);
    }
}
