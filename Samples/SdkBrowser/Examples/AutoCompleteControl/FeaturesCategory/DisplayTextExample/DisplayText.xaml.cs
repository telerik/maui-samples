using Microsoft.Maui.Controls;
using SDKBrowserMaui.Examples.AutoCompleteControl.ViewModels;

namespace SDKBrowserMaui.Examples.AutoCompleteControl.FeaturesCategory.DisplayTextExample;
public partial class DisplayText : ContentView
{
	public DisplayText()
	{
		InitializeComponent();
        this.BindingContext = new ClientsViewModel();

        this.autoCompleteView.AutomationId = "plainModeAutoCompleteView";
        this.radAutoCompleteView.AutomationId = "tokensModeAutoCompleteView";
    }
}