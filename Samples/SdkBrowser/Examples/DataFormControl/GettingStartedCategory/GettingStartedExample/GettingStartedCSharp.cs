using Microsoft.Maui;
using Microsoft.Maui.Controls;
using Telerik.Maui.Controls;
using SDKBrowserMaui.Examples.DataFormControl.GettingStartedCategory;

namespace SDKBrowserMaui.Examples.DataFormControl.GettingStartedCategory.GettingStartedExample;

public class GettingStartedCSharp : ContentView
{
	public GettingStartedCSharp()
	{
        var content = new Grid();

        // >> dataform-gettingstarted-csharp
        var dataForm = new RadDataForm();
		dataForm.BindingContext = new GettingStartedModel();
		// << dataform-gettingstarted-csharp

		content.Add(dataForm);
		this.Content = content;
	}
}