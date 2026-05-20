using Microsoft.Maui.Controls;
using System.Linq;
using Telerik.Maui.Controls;

namespace SDKBrowserMaui.Examples.SegmentedControl.SelectionCategory.SelectionExample;

public partial class Selection : ContentView
{
    public Selection()
    {
        this.InitializeComponent();
        // >> segmentcontrol-selection-bindingcontext
        this.BindingContext = new ViewModel();
        // << segmentcontrol-selection-bindingcontext
    }

    // >> segmentcontrol-selection-event
    private void OnSelectionChanged(object sender, Telerik.Maui.RadSelectionChangedEventArgs e)
    {
        var item = e.AddedItems.FirstOrDefault();
        this.selectionItemLabel.Text = $"The new selected item is {item}.";
    }
    // << segmentcontrol-selection-event
}