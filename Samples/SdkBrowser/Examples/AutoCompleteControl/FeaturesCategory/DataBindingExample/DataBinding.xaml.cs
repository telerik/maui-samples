using Microsoft.Maui.Controls;

namespace SDKBrowserMaui.Examples.AutoCompleteControl.FeaturesCategory.DataBindingExample;

public partial class DataBinding : ContentView
{
	public DataBinding()
	{
		InitializeComponent();

        // >> autocomplete-focused
        this.autoComplete.Focused += this.AutoCompleteView_Focused;
        // << autocomplete-focused
    }

    // >> autocomplete-showsuggestions
    private void AutoCompleteView_Focused(object sender, FocusEventArgs e)
    {
        this.autoComplete.ShowSuggestions();
    }
    // << autocomplete-showsuggestions
}