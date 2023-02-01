using Microsoft.Maui.Controls;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Telerik.Maui.Controls;

namespace SDKBrowserMaui.Examples.SideDrawerControl.CommandsCategory.CustomCommandsMVVMExample;

// >> sidedrawer-commandsviewmodel-cs
public class CommandsViewModel : NotifyPropertyChangedBase
{
    private bool isDrawerOpen;

    public CommandsViewModel()
    {
        this.ChangeIsOpenCommand = new Command(this.OnChangeIsOpenExecute);
        this.CustomOpenedCommand = new CustomOpenedCommand();
        this.CustomOpeningCommand = new CustomOpeningCommand();
        this.CustomClosedCommand = new CustomClosedCommand();
        this.CustomClosingCommand = new CustomClosingCommand();
        this.InvokedCommands = new ObservableCollection<string>();
    }

    public bool IsDrawerOpen
    {
        get
        {
            return this.isDrawerOpen;
        }
        set
        {
            this.UpdateValue(ref this.isDrawerOpen, value);
        }
    }

    public ICommand ChangeIsOpenCommand { get; set; }

    public ICommand CustomOpenedCommand { get; set; }

    public ICommand CustomOpeningCommand { get; set; }

    public ICommand CustomClosedCommand { get; set; }

    public ICommand CustomClosingCommand { get; set; }

    public ObservableCollection<string> InvokedCommands { get; set; }

    private void OnChangeIsOpenExecute()
    {
        this.IsDrawerOpen = !this.IsDrawerOpen;
    }
}
// << sidedrawer-commandsviewmodel-cs
