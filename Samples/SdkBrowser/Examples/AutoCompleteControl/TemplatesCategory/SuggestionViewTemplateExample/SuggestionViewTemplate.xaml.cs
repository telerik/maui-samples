using Microsoft.Maui.Controls;
using System.Linq;
using SDKBrowserMaui.Examples.AutoCompleteControl.Models;

namespace SDKBrowserMaui.Examples.AutoCompleteControl.TemplatesCategory.SuggestionViewTemplateExample;

public partial class SuggestionViewTemplate : ContentView
{
	public SuggestionViewTemplate()
	{
		InitializeComponent();
	}

    private void DataGrid_SelectionChanged(object sender, Telerik.Maui.Controls.Compatibility.DataGrid.DataGridSelectionChangedEventArgs e)
    {
        var item = e.AddedItems.FirstOrDefault() as Person;
        if (item == null)
        {
            return;
        }

        this.autoComplete.HideSuggestions();
        this.autoComplete.Tokens.Add(item);
        this.autoComplete.Text = string.Empty;
    }
}