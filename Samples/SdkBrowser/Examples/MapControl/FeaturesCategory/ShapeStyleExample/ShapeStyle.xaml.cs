using Microsoft.Maui.Controls.Xaml;
using Telerik.Maui.Controls;
using Telerik.Maui.Controls.Compatibility.Map;

namespace SDKBrowserMaui.Examples.MapControl.FeaturesCategory.ShapeStyleExample
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ShapeStyle : RadContentView
    {
        public ShapeStyle()
        {
            InitializeComponent();

            var assembly = this.GetType().Assembly;
            var source = MapSource.FromResource("SDKBrowserMaui.Examples.MapControl.world.shp", assembly);
            this.reader.Source = source;
        }
    }
}