using Microsoft.Maui.Controls.Xaml;
using Telerik.Maui.Controls;

namespace SDKBrowserMaui.Examples.DataGridControl.SelectionCategory.UISelectionExample;

    [XamlCompilation(XamlCompilationOptions.Compile)]
public partial class GridUISelection : RadContentView
{
    public GridUISelection()
    {
        this.InitializeComponent();

        this.BindingContext = new ViewModel();
    }
}