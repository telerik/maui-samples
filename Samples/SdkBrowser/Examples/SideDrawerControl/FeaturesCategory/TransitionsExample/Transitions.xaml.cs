using Microsoft.Maui.Controls.Xaml;
using Telerik.Maui.Controls;
using System.Collections.Generic;
using System;
using System.Linq;

namespace SDKBrowserMaui.Examples.SideDrawerControl.FeaturesCategory.TransitionsExample
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Transitions : RadContentView
    { 
        public Transitions()
        {
            this.InitializeComponent();
        // >> sidedrawer-transition-cs
            this.drawerList.ItemsSource = new[]
            {
                    "Inbox",
                    "Drafts",
                    "Sent Items"
            };

            this.picker.ItemsSource = Enum.GetValues(typeof(SideDrawerTransitionType)).OfType<SideDrawerTransitionType>().Where(item => item != SideDrawerTransitionType.Custom).ToList();
            this.picker.SelectedItem = SideDrawerTransitionType.Push;
        // << sidedrawer-transition-cs
        }

        private void OnButtonClicked(object sender, EventArgs e)
        {
            this.drawer.IsOpen = !this.drawer.IsOpen;
        }
    }
}