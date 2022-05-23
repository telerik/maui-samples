using Microsoft.Maui;
using Microsoft.Maui.Controls;
using Telerik.Maui.Controls;


namespace SDKBrowserMaui.Examples.TabViewControl.GettingStartedCategory.GettingStartedExample
{
    public class TabViewGettingStartedCSharp : ContentView
    {
        public TabViewGettingStartedCSharp()
        {
            // >> tabview-getting-started-csharp
            RadTabView tabView = new RadTabView();
            Telerik.Maui.Controls.TabViewItem homeTab = new Telerik.Maui.Controls.TabViewItem()
            {
                HeaderText = "Home",
                Content = new Label() { Text = "This is the content of the Home tab", Margin = new Thickness(10) },
            };
            Telerik.Maui.Controls.TabViewItem folderTab = new Telerik.Maui.Controls.TabViewItem()
            {
                HeaderText = "Folder",
                Content = new Label() { Text = "This is the content of the Folder tab", Margin = new Thickness(10) },
            };
            Telerik.Maui.Controls.TabViewItem viewTab = new Telerik.Maui.Controls.TabViewItem()
            {
                HeaderText = "View",
                Content = new Label() { Text = "This is the content of the View tab", Margin = new Thickness(10) },
            };
            tabView.Items.Add(homeTab);
            tabView.Items.Add(folderTab);
            tabView.Items.Add(viewTab);
            // << tabview-getting-started-csharp

            this.Content = tabView;
        }
    }
}

