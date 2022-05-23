using System;
using System.Windows.Input;

namespace SDKBrowserMaui.Examples.SideDrawerControl.CommandsCategory.CustomCommandsExample;

public class CustomCommand : ICommand
{
    // >> sidedrawer-customcommands-cs
    public event EventHandler CanExecuteChanged;

    public bool CanExecute(object parameter)
    {
        return true;
    }

    public void Execute(object parameter)
    {
        // implement some logic here
    }
    // << sidedrawer-customcommands-cs
}