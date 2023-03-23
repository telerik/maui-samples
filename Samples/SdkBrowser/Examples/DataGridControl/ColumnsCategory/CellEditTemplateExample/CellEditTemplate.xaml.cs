using Microsoft.Maui.Controls.Xaml;
using Telerik.Maui.Controls;

namespace SDKBrowserMaui.Examples.DataGridControl.ColumnsCategory.CellEditTemplateExample;

[XamlCompilation(XamlCompilationOptions.Compile)]
public partial class CellEditTemplate : RadContentView
{
    public CellEditTemplate()
    {
        InitializeComponent();

        this.BindingContext = new ViewModel();
    }

    private void StadiumCapacityChanged(object sender, Microsoft.Maui.Controls.ValueChangedEventArgs e)
    {
#if WINDOWS
        ((Microsoft.Maui.IView)this.dataGrid).InvalidateMeasure();
#endif
    }
}