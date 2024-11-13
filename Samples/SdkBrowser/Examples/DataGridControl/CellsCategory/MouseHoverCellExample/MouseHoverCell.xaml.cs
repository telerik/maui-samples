using Microsoft.Maui.Controls;
using Telerik.Maui.Controls.DataGrid;

namespace SDKBrowserMaui.Examples.DataGridControl.CellsCategory.MouseHoverCellExample;

public partial class MouseHoverCell : ContentView
{
    public MouseHoverCell()
    {
	    InitializeComponent();

        // >> datagrid-visualstateservice-propertychanged
        this.dataGrid.VisualStateService.PropertyChanged += VisualStateService_PropertyChanged;
        // << datagrid-visualstateservice-propertychanged
    }

    // >> visualstate-service
    private void VisualStateService_PropertyChanged(object? sender, System.ComponentModel.PropertyChangedEventArgs e)
    {
        if (e.PropertyName == nameof(this.dataGrid.VisualStateService.MouseHoverCell))
        {
            var cellText = this.dataGrid.VisualStateService.MouseHoverCell;
            if(cellText != null)
            {
                this.hoverCell.Text = $"Hovered cell: {cellText.Value.ToString()}";
            }
        }
    }
    // << visualstate-service
}