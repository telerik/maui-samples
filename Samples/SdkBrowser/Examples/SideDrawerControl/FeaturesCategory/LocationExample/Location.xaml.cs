using Microsoft.Maui.Controls.Xaml;
using Telerik.Maui.Controls;
using System.Collections.Generic;
using System;

namespace SDKBrowserMaui.Examples.SideDrawerControl.FeaturesCategory.LocationExample
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Location : RadContentView
    {
        public Location()
        {
            this.InitializeComponent();

            this.drawerList.ItemsSource = new[]
            {
                "Inbox",
                "Drafts",
                "Sent Items"
            };

            this.picker.ItemsSource = Enum.GetValues(typeof(SideDrawerLocation));
            this.picker.SelectedItem = SideDrawerLocation.Left;
            this.picker.SelectionChanged += this.OnSelectionChanged;
        }

        private void OnSelectionChanged(object sender, EventArgs e)
        {
            drawer.DrawerLocation = (SideDrawerLocation)this.picker.SelectedItem;
        }

        private void OnButtonClicked(object sender, EventArgs e)
        {
            drawer.IsOpen = !drawer.IsOpen;
        }
    }
}