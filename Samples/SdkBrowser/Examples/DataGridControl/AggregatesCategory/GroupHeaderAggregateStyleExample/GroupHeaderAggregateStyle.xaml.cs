using Microsoft.Maui.Controls;
using Telerik.Maui.Controls.DataGrid;

namespace SDKBrowserMaui.Examples.DataGridControl.AggregatesCategory.GroupHeaderAggregateStyleExample;

public partial class GroupHeaderAggregateStyle : ContentView
{
	public GroupHeaderAggregateStyle()
	{
		InitializeComponent();
	}

	private void OnGroupHeaderAggregatesAlignmentBtnClicked(object sender, System.EventArgs e)
	{
		this.dataGrid.GroupAggregatesAlignment = this.dataGrid.GroupAggregatesAlignment == DataGridGroupAggregatesAlignment.None
			? DataGridGroupAggregatesAlignment.NextToHeader
			: DataGridGroupAggregatesAlignment.None;
	}
}