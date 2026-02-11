using Microsoft.Maui.Controls;

namespace SDKBrowserMaui.Examples.DataGridControl.PromptControlledCategory.StylingExample;

public partial class Styling : ContentView
{
    public Styling()
    {
        InitializeComponent();

        this.BindingContext = new FlightsViewModel();
    }
}