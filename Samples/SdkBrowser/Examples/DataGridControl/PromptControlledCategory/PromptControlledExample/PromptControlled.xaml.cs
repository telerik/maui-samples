using Microsoft.Maui.Controls;

namespace SDKBrowserMaui.Examples.DataGridControl.PromptControlledCategory.PromptControlledExample;

public partial class PromptControlled : ContentView
{
    public PromptControlled()
    {
        InitializeComponent();

        this.BindingContext = new FlightsViewModel();
    }
}