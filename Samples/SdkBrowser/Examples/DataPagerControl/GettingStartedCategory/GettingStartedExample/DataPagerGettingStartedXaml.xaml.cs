using Microsoft.Maui.Controls;
using SDKBrowserMaui.Examples.DataPagerControl.FeaturesCategory;

namespace SDKBrowserMaui.Examples.DataPagerControl.GettingStartedCategory.GettingStartedExample;

public partial class DataPagerGettingStartedXaml : ContentView
{
    public DataPagerGettingStartedXaml()
    {
        InitializeComponent();
        this.BindingContext = new ViewModel();
        this.dataPager.AutomationId = "dataPager";
    }
}