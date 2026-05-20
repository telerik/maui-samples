using Microsoft.Maui.Controls;

namespace SDKBrowserMaui.Examples.SegmentedControl.DataBindingCategory.DataBindingExample;

public partial class DataBinding : ContentView
{
    public DataBinding()
    {
        this.InitializeComponent();
        this.BindingContext = new ViewModel();
    }
}