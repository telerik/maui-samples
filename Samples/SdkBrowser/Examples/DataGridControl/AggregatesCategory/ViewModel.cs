using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDKBrowserMaui.Examples.DataGridControl.AggregatesCategory;

// >> datagrid-aggregates-viewmodel
public class ViewModel
{
    public ViewModel()
    {
        this.Peripherals = new ObservableCollection<Data>
        {
            new Data { Name = "KeyBoard", Price = 24.6, DeliveryPrice = 2, Quantity = 32 },
            new Data { Name = "Mouse", Price = 30.9, DeliveryPrice = 2, Quantity = 54 },
            new Data { Name = "Video Card", Price = 760.7, DeliveryPrice = 3, Quantity = 17 },
            new Data { Name = "Motherboard", Price = 210.4, DeliveryPrice = 4, Quantity = 12 },
            new Data { Name = "SSD", Price = 42.9, DeliveryPrice = 3, Quantity = 88 },
            new Data { Name = "RAM", Price = 50, DeliveryPrice = 4, Quantity = 126 }
        };
    }

    public ObservableCollection<Data> Peripherals { get; set; }
}
// << datagrid-aggregates-viewmodel
