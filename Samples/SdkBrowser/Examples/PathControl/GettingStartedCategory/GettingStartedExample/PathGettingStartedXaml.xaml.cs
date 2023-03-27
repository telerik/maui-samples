using System;
using Microsoft.Maui.Controls.Xaml;
using Telerik.Maui.Controls;

namespace SDKBrowserMaui.Examples.PathControl.GettingStartedCategory.GettingStartedExample
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PathGettingStartedXaml : RadContentView
    {
        public PathGettingStartedXaml()
        {
            InitializeComponent();

            this.rootGrid.SizeChanged += RootGridSizeChanged;
        }

        private void RootGridSizeChanged(object sender, EventArgs e)
        {
            double size = Math.Min(this.rootGrid.Width, this.rootGrid.Height / 2);

            this.starPath.WidthRequest = size;
            this.starPath.HeightRequest = size;
            this.customPath.WidthRequest = size;
            this.customPath.HeightRequest = size;
        }
    }
}