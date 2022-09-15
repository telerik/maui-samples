using Microsoft.Maui;
using Microsoft.Maui.Controls;

namespace SDKBrowserMaui.Examples.DataFormControl.CommitCategory.CommitModeExample;

public partial class CommitMode : ContentView
{
	public CommitMode()
	{
		InitializeComponent();
	}

	// >> dataform-commit-changes
	private void commitNameButton_Clicked(object sender, System.EventArgs e)
	{
		this.dataForm.CommitChanges();
	}
    // << dataform-commit-changes
}