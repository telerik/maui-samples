using QSF.ViewModels;
using QSF.Examples.DataGridControl.Common;
using System.Collections.ObjectModel;
using Microsoft.Maui.Graphics;

namespace QSF.Examples.DataGridControl.CustomizationExample
{
    public class CustomizationViewModel : ExampleViewModel
    {
        public ObservableCollection<SalesPerson> People { get; private set; }

        public CustomizationViewModel()
        {
            this.People = DataGenerator.GetItems<ObservableCollection<SalesPerson>>(ResourcePaths.PeoplePath);
        }
    }
}
