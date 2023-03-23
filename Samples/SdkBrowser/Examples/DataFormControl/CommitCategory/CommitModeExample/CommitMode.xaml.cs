using Microsoft.Maui;
using Microsoft.Maui.Controls;

namespace SDKBrowserMaui.Examples.DataFormControl.CommitCategory.CommitModeExample;

public partial class CommitMode : ContentView
{
	public CommitMode()
	{
		InitializeComponent();
	}

	
	private void OnCommitClicked(object sender, System.EventArgs e)
	{
        var propertyName = "FisrtName";

        if (string.IsNullOrEmpty(propertyName))
        {
            // >> dataform-commit-changes
            this.dataForm.CommitChanges();
            // << dataform-commit-changes
        }
        else
        {
            // >> dataform-commit-changes-on-property
            this.dataForm.CommitChanges(propertyName);
            // << dataform-commit-changes-on-property
        }
    }

    private void OnCancelClicked(object sender, System.EventArgs e)
    {
        var propertyName = "FirstName";

        if (string.IsNullOrEmpty(propertyName))
        {
            this.dataForm.CancelChanges();
        }
        else
        {
            this.dataForm.CancelChanges(propertyName);
        }
    }
}