using Microsoft.Maui.Controls.Xaml;
using Telerik.Maui.Controls;
using System.Collections.Generic;
using System;

namespace SDKBrowserMaui.Examples.SideDrawerControl.GettingStartedCategory.GettingStartedExample
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SideDrawerGettingStartedXaml : RadContentView
    {
        public SideDrawerGettingStartedXaml()
        {
            InitializeComponent();
        }

        private void OnButtonClicked(object sender, EventArgs e)
        {
            drawer.IsOpen = !drawer.IsOpen;
        }
    }
}