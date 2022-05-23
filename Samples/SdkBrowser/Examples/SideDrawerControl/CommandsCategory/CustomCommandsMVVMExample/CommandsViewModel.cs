using SDKBrowserMaui.Examples.SideDrawerControl.CommandsCategory.CustomCommandsExample;
using System.Windows.Input;

namespace SDKBrowserMaui.Examples.SideDrawerControl.CommandsCategory.CustomCommandsMVVMExample
{
    // >> sidedrawer-commandsviewmodel-cs
    public class CommandsViewModel
    {
        public CommandsViewModel()
        {
            this.Command = new CustomCommand();
        }

        public ICommand Command { get; set; }
    }
    // << sidedrawer-commandsviewmodel-cs
}
