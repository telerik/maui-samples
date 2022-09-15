using Microsoft.Maui;
using Microsoft.Maui.Controls;
using SDKBrowserMaui.Examples.DataFormControl.GettingStartedCategory;

namespace SDKBrowserMaui.Examples.DataFormControl.GettingStartedCategory.GettingStartedExample;

public partial class GettingStartedXaml : ContentView
{
	public GettingStartedXaml()
	{
		InitializeComponent();

		this.dataForm.BindingContext = new GettingStartedModel();
    }
}