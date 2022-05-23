using Microsoft.Maui.Controls.Xaml;
using System;
using Telerik.Maui.Controls;


namespace QSF.Examples.SideDrawerControl.FirstLookExample
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FirstLookView : RadContentView
    {
        public FirstLookView()
        {
            InitializeComponent();
        }

        private void OpenDrawer(object sender, EventArgs e)
        {
            this.drawer.IsOpen = !this.drawer.IsOpen;
        }
    }
}