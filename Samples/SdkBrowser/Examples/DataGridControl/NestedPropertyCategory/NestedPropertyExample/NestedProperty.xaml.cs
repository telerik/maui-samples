using Microsoft.Maui.Controls.Xaml;
using Telerik.Maui.Controls;

namespace SDKBrowserMaui.Examples.DataGridControl.NestedPropertyCategory.NestedPropertyExample;

[XamlCompilation(XamlCompilationOptions.Compile)]
public partial class NestedProperty : RadContentView
{
    public NestedProperty ()
    {
        InitializeComponent ();
        
        this.BindingContext = new ViewModel();
    }
}