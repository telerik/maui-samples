using Microsoft.Maui.Controls.Xaml;
using Microsoft.Maui.Controls;
using Telerik.Maui.Controls;

namespace SDKBrowserMaui.Examples.BusyIndicatorControl.IntegrationCategory.CollectionViewIntegrationExample
{
    public partial class CollectionViewIntegration : ContentView
    {
        public CollectionViewIntegration()
        {
            InitializeComponent();

            // >> busyindicator-withcollectionview-setvm
            this.BindingContext = new ViewModel();
            // << busyindicator-withcollectionview-setvm
        }
    }
}
