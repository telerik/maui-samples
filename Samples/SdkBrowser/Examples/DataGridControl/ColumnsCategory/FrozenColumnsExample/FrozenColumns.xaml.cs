using Microsoft.Maui.Controls;

namespace SDKBrowserMaui.Examples.DataGridControl.ColumnsCategory.FrozenColumnsExample;

public partial class FrozenColumns : ContentView
{
    public FrozenColumns()
	{
		InitializeComponent();
	}

    private void OnFirstColumnFrozenClicked(object sender, System.EventArgs e)
    {
        // >> data-grid-frozen-columns-programmatically
        this.grid.Columns[0].IsFrozen = !this.grid.Columns[0].IsFrozen;
        // << data-grid-frozen-columns-programmatically
    }

    private void OnLastColumnFrozenClicked(object sender, System.EventArgs e)
    {
        this.grid.Columns[this.grid.Columns.Count - 1].IsFrozen = !this.grid.Columns[this.grid.Columns.Count - 1].IsFrozen;
    }
}