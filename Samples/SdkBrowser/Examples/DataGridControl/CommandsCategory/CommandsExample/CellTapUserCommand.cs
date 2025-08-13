using Microsoft.Maui.Controls;
using Telerik.Maui.Controls.DataGrid;

namespace SDKBrowserMaui.Examples.DataGridControl.CommandsCategory.CommandsExample;

// >> datagrid-commands-celltap
public class CellTapUserCommand : DataGridCommand
{
    public CellTapUserCommand()
    {
        Id = DataGridCommandId.CellTap;
    }
    public override bool CanExecute(object parameter)
    {
        return true;
    }
    public override void Execute(object parameter)
    {
        var context = parameter as DataGridCellInfo;
        var cellTap =  $"You tapped on {context.Value} inside {context.Column.HeaderText} column \n";
        Application.Current.Windows[0].Page.DisplayAlert("CellTap Command: ", cellTap, "OK");
        this.Owner.CommandService.ExecuteDefaultCommand(DataGridCommandId.CellTap, parameter);
    }
}
// << datagrid-commands-celltap