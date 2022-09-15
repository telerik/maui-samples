using Microsoft.Maui;
using Microsoft.Maui.Controls;

namespace SDKBrowserMaui.Examples.DataFormControl.ValidationCategory.ValidationConfigurationExample;

public partial class ValidationConfiguration : ContentView
{
	public ValidationConfiguration()
	{
		InitializeComponent();
	}
    private void OnValidateChangesClicked(object sender, System.EventArgs e)
    {
        this.dataForm.ValidateChanges();
    }
}