using Microsoft.Maui.Controls.Xaml;
using Telerik.Maui.Controls;
using Telerik.Maui.Controls.Compatibility.Map;

namespace SDKBrowserMaui.Examples.MapControl.GettingStartedCategory.GettingStartedExample
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MapGettingStartedXaml : RadContentView
    {
        public MapGettingStartedXaml()
        {
            InitializeComponent();

            // >> map-gettingstarted-setting-source
            var assembly = this.GetType().Assembly;
            var source = MapSource.FromResource("SDKBrowserMaui.Examples.MapControl.world.shp", assembly);
            this.reader.Source = source;
            // << map-gettingstarted-setting-source
        }
    }
}