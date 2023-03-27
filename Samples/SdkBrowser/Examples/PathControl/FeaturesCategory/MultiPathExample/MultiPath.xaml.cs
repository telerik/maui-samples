using System;
using Microsoft.Maui.Controls.Xaml;
using Telerik.Maui.Controls;

namespace SDKBrowserMaui.Examples.PathControl.FeaturesCategory.MultiPathExample
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MultiPath : RadContentView
    {
        public MultiPath()
        {
            InitializeComponent();

            this.rootGrid.SizeChanged += RootGridSizeChanged;
        }

        private void RootGridSizeChanged(object sender, EventArgs e)
        {
            double size = Math.Min(this.rootGrid.Width, this.rootGrid.Height / 2);

            this.multiPath.WidthRequest = size;
            this.multiPath.HeightRequest = size;
            this.path2.WidthRequest = size;
            this.path2.HeightRequest = size;
        }
    }
}