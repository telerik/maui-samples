using Microsoft.Maui.Controls;
using Telerik.Maui.Controls;
using SDKBrowserMaui.Examples.DataPagerControl.FeaturesCategory;

namespace SDKBrowserMaui.Examples.DataPagerControl.GettingStartedCategory.GettingStartedExample;

public class DataPagerGettingStartedCSharp : ContentView
{
    ViewModel vm;

    public DataPagerGettingStartedCSharp()
    {
        this.vm = new ViewModel();
        this.BindingContext = this.vm; 
        // >> datapager-gettingstarted-csharp
        var pager = new RadDataPager();
        // << datapager-gettingstarted-csharp
        pager.Source = this.vm.Data;

        var panel = new VerticalStackLayout();
        panel.Children.Add(pager);

        this.Content = panel;
    }
}