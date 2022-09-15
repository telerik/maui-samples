using Microsoft.Maui.Controls;
using SDKBrowserMaui.Examples.AutoCompleteControl.ViewModels;

namespace SDKBrowserMaui.Examples.AutoCompleteControl.FeaturesCategory.HighlightTextExample;
public partial class HighlightText : ContentView
{
	public HighlightText()
	{
		InitializeComponent();
        this.BindingContext = new ClientsViewModel();
    }
}