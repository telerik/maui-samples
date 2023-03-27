using Microsoft.Maui;
using Microsoft.Maui.Controls;

namespace SDKBrowserMaui.Examples.DataFormControl.ValidationCategory.ManualValidationExample;

public partial class ManualValidation : ContentView
{
	public ManualValidation()
	{
		InitializeComponent();
	}

	private void OnValidateChangesClicked(object sender, System.EventArgs e)
	{
        var propertyName = "FirstName";

        if (string.IsNullOrEmpty(propertyName))
        {
            // >> dataform-validatechanges
            this.dataForm.ValidateChanges();
            // << dataform-validatechanges
        }
        else
        {
            // >> dataform-validatechanges-on-property
            this.dataForm.ValidateChanges(propertyName);
            // << dataform-validatechanges-on-property
        }
    }
    

    private void OnCancelClicked(object sender, System.EventArgs e)
    {
        var propertyName = "FirstName";

        if (string.IsNullOrEmpty(propertyName))
        {
            // >> dataform-validatechanges
            this.dataForm.CancelChanges();
            // << dataform-validatechanges
        }
        else
        {
            // >> dataform-cancelchanges-on-property
            this.dataForm.CancelChanges(propertyName);
            // << dataform-cancelchanges-on-property
        }
    }
    // << dataform-validation-changes
}