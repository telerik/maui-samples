using Microsoft.Maui.Controls.Xaml;
using Telerik.Maui.Controls;

namespace SDKBrowserMaui.Examples.DataGridControl.CommandsCategory.ValidationExample;

[XamlCompilation(XamlCompilationOptions.Compile)]
public partial class Validation : RadContentView
{
    public Validation()
    {
        InitializeComponent();
        // >> datagrid-commands-validation-binding
        this.BindingContext = new ViewModel();
        this.dataGrid.Commands.Add(new ValidateCellCommand(errorContainer));
        // << datagrid-commands-validation-binding
    }
}