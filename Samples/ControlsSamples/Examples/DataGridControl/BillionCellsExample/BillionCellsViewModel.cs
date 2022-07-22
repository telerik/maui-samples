using QSF.ViewModels;
using System.Collections.ObjectModel;

namespace QSF.Examples.DataGridControl.BillionCellsExample
{
    public class BillionCellsViewModel : ExampleViewModel
    {
        public BillionCellsViewModel()
        {
            var items = new ObservableCollection<DataItem>();
            for (int i = 0; i < 10000000; i++)
            {
                items.Add(new DataItem(i));
            }
            this.Items = items;
        }
        public ObservableCollection<DataItem> Items { get; set; }

    }
}
