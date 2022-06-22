using Microsoft.Maui.Controls.Xaml;
using Telerik.Maui.Controls;
using Telerik.Maui.Controls.Compatibility.Map;
using Telerik.Maui.Controls.Compatibility.ShapefileReader;

namespace SDKBrowserMaui.Examples.MapControl.FeaturesCategory.SetBestViewExample
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SetBestView : RadContentView
    {
        public SetBestView()
        {
            InitializeComponent();

            var assembly = this.GetType().Assembly;
            var source = MapSource.FromResource("SDKBrowserMaui.Examples.MapControl.world.shp", assembly);
            this.reader.Source = source;
        }

        private void OnSetBestViewClicked(object sender, System.EventArgs e)
        {
            // >> map-setbestview-code
            var bestView = this.mapShapeLayer.GetBestView();
            this.map.SetView(bestView);
            // << map-setbestview-code
        }

        private void OnSetViewItalyClicked(object sender, System.EventArgs e)
        {
            // >> map-setview-code
            var northWest = new Location(45.7, 4.8);
            var southEast = new Location(37.7, 20.08);
            var view = new LocationRect(northWest, southEast);
            this.map.SetView(view);
            // << map-setview-code
        }
    }
}