using Microsoft.Maui.Controls;
using System.Collections.ObjectModel;
using Telerik.Maui;

namespace SDKBrowserMaui.Examples.DataGridControl.StylingCategory.RowBackgroundStyleSelectorExample;

public partial class StyleSelector : ContentView
{
	ViewModel vm;

	public StyleSelector()
	{
		InitializeComponent();

		this.vm = new ViewModel();
		this.BindingContext = this.vm;
		this.datagrid.ExpandedRowDetails = new ObservableCollection<MyData>(this.vm.Items);
	}
}