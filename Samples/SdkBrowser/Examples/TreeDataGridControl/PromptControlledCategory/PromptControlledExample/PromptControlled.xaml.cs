using Microsoft.Maui.Controls;

namespace SDKBrowserMaui.Examples.TreeDataGridControl.PromptControlledCategory.PromptControlledExample;

public partial class PromptControlled : ContentView
{
    public PromptControlled()
    {
        InitializeComponent();

        this.BindingContext = new ViewModel();
    }
}