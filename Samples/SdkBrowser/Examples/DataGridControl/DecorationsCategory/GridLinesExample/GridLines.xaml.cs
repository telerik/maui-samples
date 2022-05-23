using Microsoft.Maui.Controls.Xaml;
using Telerik.Maui.Controls;

namespace SDKBrowserMaui.Examples.DataGridControl.DecorationsCategory.GridLinesExample;

[XamlCompilation(XamlCompilationOptions.Compile)]
public partial class GridLines : RadContentView
{
    public GridLines()
    {
        this.InitializeComponent();

        this.BindingContext = new ViewModel();
    }
}