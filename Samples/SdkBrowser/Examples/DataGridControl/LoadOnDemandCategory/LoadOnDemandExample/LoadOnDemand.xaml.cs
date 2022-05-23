using Microsoft.Maui.Controls.Xaml;
using Telerik.Maui.Controls;

namespace SDKBrowserMaui.Examples.DataGridControl.LoadOnDemandCategory.LoadOnDemandExample;

[XamlCompilation(XamlCompilationOptions.Compile)]
public partial class LoadOnDemand : RadContentView
{
    public LoadOnDemand()
    {
        this.InitializeComponent();

        this.BindingContext = new LoadOnDemandViewModel();
    }
}