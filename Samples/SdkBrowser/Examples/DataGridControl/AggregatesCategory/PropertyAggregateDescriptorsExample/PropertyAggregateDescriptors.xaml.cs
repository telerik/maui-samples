using Microsoft.Maui.Controls;
using Microsoft.Maui.Controls.Xaml;
using System.Collections.Generic;

namespace SDKBrowserMaui.Examples.DataGridControl.AggregatesCategory.PropertyAggregateDescriptorsExample;

[XamlCompilation(XamlCompilationOptions.Compile)]
public partial class PropertyAggregateDescriptors : ContentView
{
    public PropertyAggregateDescriptors()
    {
        InitializeComponent();

        this.dataGrid.ItemsSource = new List<Data>
        {
            new Data { Name = "KeyBoard", Price = 24.6, DeliveryPrice = 2, Quantity = 32 },
            new Data { Name = "Mouse", Price = 30.9, DeliveryPrice = 2, Quantity = 54 },
            new Data { Name = "Video Card", Price = 760.7, DeliveryPrice = 3, Quantity = 17 },
            new Data { Name = "Motherboard", Price = 210.4, DeliveryPrice = 4, Quantity = 12 },
            new Data { Name = "SSD", Price = 42.9, DeliveryPrice = 3, Quantity = 88 },
            new Data { Name = "RAM", Price = 50, DeliveryPrice = 4, Quantity = 126 }
        };
    }
}