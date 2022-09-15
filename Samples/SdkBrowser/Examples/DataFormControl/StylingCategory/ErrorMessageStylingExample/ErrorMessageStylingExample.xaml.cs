using Microsoft.Maui.Controls;
using Microsoft.Maui.Graphics;

namespace SDKBrowserMaui.Examples.DataFormControl.StylingCategory.ErrorMessageStylingExample;

public partial class ErrorMessageStylingExample : ContentView
{
	public ErrorMessageStylingExample()
	{
		InitializeComponent();

        this.Loaded += (sender, args) =>
        {
            this.dataFormCommonErrorStyle.ValidateChanges();
            this.dataFormIndividualErrorStyle.ValidateChanges();
        };
	}
}