using Microsoft.Maui;
using Microsoft.Maui.Controls;

namespace SDKBrowserMaui.Examples.DataFormControl.ValidationCategory.ValidationModeExample;

public partial class ValidationMode : ContentView
{
	public ValidationMode()
	{
		InitializeComponent();
	}

	// >> dataform-validation-changes
	private void OnValidateChangesClicked(object sender, System.EventArgs e)
	{
        this.dataForm.ValidateChanges();
    }
    // << dataform-validation-changes
}