using Microsoft.Maui.Controls;
using Telerik.Maui.Controls;

namespace SDKBrowserMaui.Examples.SegmentedControl.SelectionCategory.SelectionExample;

public partial class Selection : ContentView
{
    public Selection()
    {
        InitializeComponent();
        // >> segmentcontrol-selection-bindingcontext
        this.segmentControl.BindingContext = new ViewModel();
        // << segmentcontrol-selection-bindingcontext
    }
    // >> segmentcontrol-selection-event
    private void SelectionChanged(object sender, ValueChangedEventArgs<int> e)
    {
        this.selectionItemLabel.Text = $"The new selected item index is {e.NewValue}.";
    }
    // << segmentcontrol-selection-event
}