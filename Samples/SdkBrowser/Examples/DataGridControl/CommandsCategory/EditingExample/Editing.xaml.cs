using Microsoft.Maui.Controls.Xaml;
using Telerik.Maui.Controls;

namespace SDKBrowserMaui.Examples.DataGridControl.CommandsCategory.EditingExample;

[XamlCompilation(XamlCompilationOptions.Compile)]
public partial class Editing : RadContentView
{
    public Editing()
    {
        InitializeComponent();
        // >> datagrid-commands-editing-binding
        this.BindingContext = new ViewModel();
        dataGrid.Commands.Add(new BeginEditCommand());
        dataGrid.Commands.Add(new CommitEditCommand());
        // << datagrid-commands-editing-binding
    }
}