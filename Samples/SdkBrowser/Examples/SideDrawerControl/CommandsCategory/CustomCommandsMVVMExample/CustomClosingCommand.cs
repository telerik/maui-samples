using System;
using System.Windows.Input;

namespace SDKBrowserMaui.Examples.SideDrawerControl.CommandsCategory.CustomCommandsMVVMExample;

public class CustomClosingCommand : ICommand
{
    public event EventHandler CanExecuteChanged;

    public bool CanExecute(object parameter)
    {
        return true;
    }

    public void Execute(object parameter)
    {
        var viewModel = parameter as CommandsViewModel;

        if (viewModel != null)
        {
            viewModel.InvokedCommands.Add("Invoked Command: Closing");
        }
    }
}
