using System;
using System.Linq;
using Telerik.Maui.Controls;
using Microsoft.Maui.Controls.Xaml;
using Microsoft.Maui.Controls;

namespace SDKBrowserMaui.Examples.DataGridControl.DataTableCategory.CommandsExample;

[XamlCompilation(XamlCompilationOptions.Compile)]
public partial class Commands : RadContentView
{
    DataTableViewModel viewModel;
    public Commands()
	{
		InitializeComponent();

		this.viewModel = new DataTableViewModel();
		this.BindingContext = viewModel;
    }
}