using Microsoft.Maui.Controls.Xaml;
using Telerik.Maui.Controls;

namespace SDKBrowserMaui.Examples.DataGridControl.LoadOnDemandCategory.LoadMoreDataCommandExample;

[XamlCompilation(XamlCompilationOptions.Compile)]
public partial class LoadMoreDataCommand : RadContentView
{
    public LoadMoreDataCommand()
    {
        this.InitializeComponent();
        
        this.BindingContext = new LoadMoreDataCommandViewModel();
        // >> datagrid-customloadmoredatacommand-addtocollection-csharp
        this.dataGrid.Commands.Add(new CustomLoadMoreDataCommand());
        // << datagrid-customloadmoredatacommand-addtocollection-csharp
    }

}