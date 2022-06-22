using Microsoft.Maui.Controls;
using Telerik.Maui.Controls.Compatibility.DataGrid.Commands;

namespace SDKBrowserMaui.Examples.DataGridControl.CommandsCategory.EditingExample;

// >> datagrid-commands-editing-commitedit
public class CommitEditCommand : DataGridCommand
{
    public CommitEditCommand()
    {
        this.Id = DataGridCommandId.CommitEdit;
    }

    public override void Execute(object parameter)
    {
        var context = (EditContext)parameter;
        
        App.DisplayAlert("Edit Committed");
        this.Owner.CommandService.ExecuteDefaultCommand(DataGridCommandId.CommitEdit, parameter);
    }
}
// << datagrid-commands-editing-commitedit