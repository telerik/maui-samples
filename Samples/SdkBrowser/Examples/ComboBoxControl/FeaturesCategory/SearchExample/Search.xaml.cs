using Microsoft.Maui.Controls.Xaml;
using Telerik.Maui.Controls;

namespace SDKBrowserMaui.Examples.ComboBoxControl.FeaturesCategory.SearchExample;

[XamlCompilation(XamlCompilationOptions.Compile)]
public partial class Search : RadContentView
{
    public Search()
    {
        InitializeComponent();
        this.BindingContext = new ViewModel();
    }
}
