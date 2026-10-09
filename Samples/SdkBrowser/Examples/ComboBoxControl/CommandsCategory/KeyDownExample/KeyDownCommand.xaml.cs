using Microsoft.Maui.Controls;

namespace SDKBrowserMaui.Examples.ComboBoxControl.CommandsCategory.KeyDownExample;

public partial class KeyDownCommand : ContentView
{
    public KeyDownCommand()
    {
        InitializeComponent();

        this.BindingContext = new ViewModel();
    }
}
