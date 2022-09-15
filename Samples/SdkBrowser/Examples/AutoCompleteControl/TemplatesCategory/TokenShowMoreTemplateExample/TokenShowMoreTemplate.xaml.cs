using System;
using Microsoft.Maui;
using Microsoft.Maui.Controls;
using SDKBrowserMaui.Examples.AutoCompleteControl.Models;

namespace SDKBrowserMaui.Examples.AutoCompleteControl.TemplatesCategory.TokenShowMoreTemplateExample;

public partial class TokenShowMoreTemplate : ContentView
{
	public TokenShowMoreTemplate()
	{
		InitializeComponent();
    }

    // >> autocompleteview-templates-token-template-labelgesture
    private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
    {
        var closeLabel = sender as Label;
        if (closeLabel != null)
        {
            var item = closeLabel.BindingContext as City;
            if (item != null)
            {
                this.autoCompleteTokenTemplate.Tokens.Remove(item);
            }
        }
    }
    // << autocompleteview-templates-token-template-labelgesture
}