using Microsoft.Maui.Controls.Xaml;
using Telerik.Maui.Controls;
using Telerik.Maui.Controls.Compatibility.Map;

namespace SDKBrowserMaui.Examples.MapControl.FeaturesCategory.ProgrammaticZoomExample
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ProgrammaticZoom : RadContentView
    {
        public ProgrammaticZoom()
        {
            InitializeComponent();

            var assembly = this.GetType().Assembly;
            var source = MapSource.FromResource("SDKBrowserMaui.Examples.MapControl.world.shp", assembly);
            this.reader.Source = source;
        }

        private void OnEntryCompleted(object sender, System.EventArgs e)
        {
            var text = this.zoomLevelEntry.Text;
            int zoomLevel;
            if (int.TryParse(text, out zoomLevel))
            {
                this.map.ZoomToLevel(zoomLevel);
            }
        }
    }
}