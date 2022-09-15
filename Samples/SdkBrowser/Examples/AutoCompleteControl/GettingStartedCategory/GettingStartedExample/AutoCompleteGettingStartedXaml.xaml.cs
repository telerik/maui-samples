using Microsoft.Maui.Controls;
using System.Collections.Generic;

namespace SDKBrowserMaui.Examples.AutoCompleteControl.GettingStartedCategory.GettingStartedExample;

public partial class AutoCompleteGettingStartedXaml : ContentView
{
	public AutoCompleteGettingStartedXaml()
	{
		InitializeComponent();
        // >> autocomplete-getting-started-items-source
        this.autoComplete.ItemsSource = new List<string>()
            {
                "Freda Curtis",
                "Jeffery Francis",
                "Eva Lawson",
                "Emmett Santos",
                "Theresa Bryan",
                "Jenny Fuller",
                "Terrell Norris",
                "Eric Wheeler",
                "Julius Clayton",
                "Alfredo Thornton",
                "Roberto Romero",
                "Orlando Mathis",
                "Eduardo Thomas",
                "Harry Douglas"
            };
        // << autocomplete-getting-started-items-source
    }
}