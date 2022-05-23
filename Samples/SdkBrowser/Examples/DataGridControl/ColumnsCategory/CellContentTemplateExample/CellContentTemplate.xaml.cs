using Microsoft.Maui.Controls.Xaml;
using Telerik.Maui.Controls;

namespace SDKBrowserMaui.Examples.DataGridControl.ColumnsCategory.CellContentTemplateExample;

[XamlCompilation(XamlCompilationOptions.Compile)]
public partial class CellContentTemplate : RadContentView
{
    public CellContentTemplate()
    {
        InitializeComponent();

        this.BindingContext = new ViewModel();
    }
}