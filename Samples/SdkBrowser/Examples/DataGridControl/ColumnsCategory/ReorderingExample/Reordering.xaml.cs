using Microsoft.Maui.Controls;

namespace SDKBrowserMaui.Examples.DataGridControl.ColumnsCategory.ReorderingExample;

public partial class Reordering : ContentView
{
    public Reordering()
    {
        InitializeComponent();
        this.BindingContext = new ViewModel();
    }
}