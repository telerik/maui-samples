using Microsoft.Maui.Controls;
using SDKBrowserMaui.Examples.AutoCompleteControl.ViewModels;

namespace SDKBrowserMaui.Examples.AutoCompleteControl.CommandsCategory.ClearTextExample;

public partial class ClearTextCommand : ContentView
{
    public ClearTextCommand()
    {
        InitializeComponent();

        this.BindingContext = new ClientsViewModel();
    }
}
