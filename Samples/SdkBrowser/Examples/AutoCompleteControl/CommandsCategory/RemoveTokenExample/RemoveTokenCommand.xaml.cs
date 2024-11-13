using Microsoft.Maui.Controls;
using SDKBrowserMaui.Examples.AutoCompleteControl.ViewModels;

namespace SDKBrowserMaui.Examples.AutoCompleteControl.CommandsCategory.RemoveTokenExample;

public partial class RemoveTokenCommand : ContentView
{
	public RemoveTokenCommand()
	{
		InitializeComponent();

		this.BindingContext = new ClientsViewModel();
	}
}