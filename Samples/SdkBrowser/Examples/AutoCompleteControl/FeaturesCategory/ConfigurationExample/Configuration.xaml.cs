using Microsoft.Maui.Controls;
using SDKBrowserMaui.Examples.AutoCompleteControl.ViewModels;

namespace SDKBrowserMaui.Examples.AutoCompleteControl.FeaturesCategory.ConfigurationExample;
public partial class Configuration : ContentView
{
	public Configuration()
	{
		InitializeComponent();
        this.BindingContext = new ExtendedClientsViewModel();
    }
}