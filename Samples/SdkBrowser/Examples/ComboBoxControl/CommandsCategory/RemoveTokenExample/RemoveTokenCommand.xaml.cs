using Microsoft.Maui.Controls;

namespace SDKBrowserMaui.Examples.ComboBoxControl.CommandsCategory.RemoveTokenExample;

public partial class RemoveTokenCommand : ContentView
{
    public RemoveTokenCommand()
    {
        InitializeComponent();

        this.BindingContext = new ViewModel();
    }
}