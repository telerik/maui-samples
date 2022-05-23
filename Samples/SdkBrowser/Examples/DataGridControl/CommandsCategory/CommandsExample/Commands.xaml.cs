using Microsoft.Maui.Controls.Xaml;
using System.Collections.ObjectModel;
using Telerik.Maui.Controls;

namespace SDKBrowserMaui.Examples.DataGridControl.CommandsCategory.CommandsExample;

[XamlCompilation(XamlCompilationOptions.Compile)]
public partial class Commands : RadContentView
{
    public Commands()
    {
        InitializeComponent();
        // >> datagrid-commands-celltap-data
        var source = new ObservableCollection<Country>();
        source.Add(new Country("Mozambique", 24692000));
        source.Add(new Country("Paraguay", 6725000));
        source.Add(new Country("Turkmenistan", 5663000));
        source.Add(new Country("Mongolia", 3027000));
        source.Add(new Country("Japan", 127000000));
        source.Add(new Country("Bulgaria", 7128000));
        source.Add(new Country("Chad", 14450000));
        source.Add(new Country("Netherlands", 17020000));

        this.BindingContext = source;
        // << datagrid-commands-celltap-data
        // >> datagrid-commands-cetttap-add
        grid.Commands.Add(new CellTapUserCommand());
        // << datagrid-commands-cetttap-add
    }
}