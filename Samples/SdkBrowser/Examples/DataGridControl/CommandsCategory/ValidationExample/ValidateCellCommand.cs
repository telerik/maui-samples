using Microsoft.Maui.Controls;
using Telerik.Maui.Controls.Compatibility.DataGrid.Commands;

namespace SDKBrowserMaui.Examples.DataGridControl.CommandsCategory.ValidationExample;

// >> datagrid-commands-validation-validatecell
public class ValidateCellCommand : DataGridCommand
{
    Grid grid;
    public ValidateCellCommand(Grid grid)
    {
        this.Id = DataGridCommandId.ValidateCell;
        this.grid = grid;
    }

    public override void Execute(object parameter)
    {
        var context = (ValidateCellContext)parameter;
        this.grid.IsVisible = context.Errors.Count > 0;
    }
}
// << datagrid-commands-validation-validatecell