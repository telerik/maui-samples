using Microsoft.Maui.Controls;
using Telerik.Maui.Controls;
using Microsoft.Maui;

namespace SDKBrowserMaui.Examples.TabViewControl.FeaturesCategory.SelectionExample
{
    public class Selection : ContentView
    {
        public Selection()
        {
            // >> tabview-features-selection-csharp
            RadTabView tabView = new RadTabView();
            tabView.Items.Add(new Telerik.Maui.Controls.TabViewItem() { HeaderText = "Home" });
            tabView.Items.Add(new Telerik.Maui.Controls.TabViewItem() { HeaderText = "Folder" });
            tabView.Items.Add(new Telerik.Maui.Controls.TabViewItem() { HeaderText = "View" });

            tabView.SelectedItem = tabView.Items[1];
            // << tabview-features-selection-csharp

            this.Content = tabView;
        }
    }
}
