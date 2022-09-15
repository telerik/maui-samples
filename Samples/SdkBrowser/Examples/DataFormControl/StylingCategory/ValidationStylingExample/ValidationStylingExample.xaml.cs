using Microsoft.Maui.Controls;

namespace SDKBrowserMaui.Examples.DataFormControl.StylingCategory.ValidationStylingExample;

public partial class ValidationStylingExample : ContentView
{
	public ValidationStylingExample()
	{
		InitializeComponent();

        this.Loaded += (sender, args) =>
        {
            this.dataForm.ValidateChanges();
        };
    }
}