using SDKBrowser.Examples.DataGridControl;
using Telerik.Maui.Controls.Compatibility.DataGrid;
using Telerik.Maui.Controls.Compatibility.DataGrid.Commands;

namespace SDKBrowserMaui.Examples.DataGridControl.LoadOnDemandCategory.LoadMoreDataCommandExample;

// >> datagrid-customloadmoredatacommand-csharp
public class CustomLoadMoreDataCommand : DataGridCommand
{
    public CustomLoadMoreDataCommand()
    {
        this.Id = DataGridCommandId.LoadMoreData;
    }

    public override bool CanExecute(object parameter)
    {
        return true;
    }

    public async override void Execute(object parameter)
    {
        if (parameter == null)
        {
            return;
        }

        ((LoadOnDemandContext)parameter).ShowLoadOnDemandLoadingIndicator();
        
        await System.Threading.Tasks.Task.Delay(1500);
        var viewModel = this.Owner.BindingContext as LoadMoreDataCommandViewModel;
        if (viewModel != null)
        {
            for (int i = 0; i < 10; i++)
            {
                viewModel.Items.Add(new Person { Name = "Person " + i, Age = i + 18, Gender = i % 2 == 0 ? Gender.Male : Gender.Female });
            }
        }
        ((LoadOnDemandContext)parameter).HideLoadOnDemandLoadingIndicator();
    }
}
// << datagrid-customloadmoredatacommand-csharp
