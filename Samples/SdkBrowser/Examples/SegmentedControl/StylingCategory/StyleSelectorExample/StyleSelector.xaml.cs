using Microsoft.Maui.Controls;

namespace SDKBrowserMaui.Examples.SegmentedControl.StylingCategory.StyleSelectorExample;

public partial class StyleSelector : ContentView
{
    public StyleSelector()
    {
        this.InitializeComponent();
        this.BindingContext = new ViewModel();
    }
}