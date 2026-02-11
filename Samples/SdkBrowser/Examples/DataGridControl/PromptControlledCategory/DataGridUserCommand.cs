using Microsoft.Maui.Controls;
using System.Windows.Input;
using Telerik.Maui.Controls.DataGrid;

namespace SDKBrowserMaui.Examples.DataGridControl.PromptControlledCategory;

public sealed class DataGridUserCommand : DataGridCommand
{
    public static readonly BindableProperty CommandProperty =
        BindableProperty.Create(nameof(Command), typeof(ICommand), typeof(DataGridUserCommand), null);

    public ICommand Command
    {
        get => (ICommand)this.GetValue(CommandProperty);
        set => this.SetValue(CommandProperty, value);
    }

    public override bool CanExecute(object parameter)
    {
        var command = this.Command;
        if (this.Owner == null || command == null)
        {
            return false;
        }

        return command.CanExecute(parameter);
    }

    public override void Execute(object parameter)
    {
        var command = this.Command;
        if (command != null)
        {
            command.Execute(parameter);
        }
    }
}
