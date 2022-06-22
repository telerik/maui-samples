using Microsoft.Maui.Controls;
using Telerik.Maui.Controls.Compatibility.DataGrid.Commands;

namespace SDKBrowserMaui.Examples.DataGridControl.CommandsCategory.EditingExample;

// >> datagrid-commands-editing-beginedit
public class BeginEditCommand : DataGridCommand
{
    public BeginEditCommand()
    {
        this.Id = DataGridCommandId.BeginEdit;
    }

    public override void Execute(object parameter)
    {
        var context = (EditContext)parameter;
        var cellEdit = $"BeginEdit on: {context.CellInfo.Value} via {context.TriggerAction} \n";
        App.DisplayAlert(cellEdit);
        this.Owner.CommandService.ExecuteDefaultCommand(DataGridCommandId.BeginEdit, parameter);
    }
}
// << datagrid-commands-editing-beginedit