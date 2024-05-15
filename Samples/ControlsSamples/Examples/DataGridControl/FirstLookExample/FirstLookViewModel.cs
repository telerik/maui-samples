using QSF.Examples.DataGridControl.Common;
using QSF.ExampleUtilities;
using QSF.ViewModels;
using System;
using System.Collections.ObjectModel;
using System.Linq;

namespace QSF.Examples.DataGridControl.FirstLookExample;

public class FirstLookViewModel : ExampleViewModel
{
    public FirstLookViewModel()
    {
        this.OrderDetails = DataGenerator.GetItems<ObservableCollection<Order>>(ResourcePaths.OrdersPath);
        this.People = DataGenerator.GetItems<ObservableCollection<SalesPerson>>(ResourcePaths.PeoplePath);
        this.JobTitles = new ObservableCollection<string>
        {
                "Sales Representative",
                "Sales Manager",
                "Sales Coordinator",
                "Vice President"
        };

        this.AssignJobTitlesToPeople();
    }

    public ObservableCollection<Order> OrderDetails { get; private set; }
    public ObservableCollection<SalesPerson> People { get; private set; }
    public ObservableCollection<string> JobTitles { get; private set; }

    private void AssignJobTitlesToPeople()
    {
        foreach (var person in this.People)
        {
            person.JobTitle = this.JobTitles.OrderBy(x => Guid.NewGuid()).FirstOrDefault();
        }
    }
}
