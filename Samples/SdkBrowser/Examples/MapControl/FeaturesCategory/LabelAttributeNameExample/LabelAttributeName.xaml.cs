using Microsoft.Maui.Controls.Xaml;
using Telerik.Maui.Controls;
using Telerik.Maui.Controls.Compatibility.Map;

namespace SDKBrowserMaui.Examples.MapControl.FeaturesCategory.LabelAttributeNameExample
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LabelAttributeName : RadContentView
    {
        public LabelAttributeName()
        {
            InitializeComponent();

            // >> map-labels-settintsource
            var assembly = this.GetType().Assembly;
            var source = MapSource.FromResource("SDKBrowserMaui.Examples.MapControl.world.shp", assembly);
            var dataSource = MapSource.FromResource("SDKBrowserMaui.Examples.MapControl.world.dbf", assembly);
            this.reader.Source = source;
            this.reader.DataSource = dataSource;
            // << map-labels-settintsource
        }

        private void OnChangeLabelAttributeNameClicked(object sender, System.EventArgs e)
        {
            if (this.mapShapeLayer.LabelAttributeName == "CNTRY_NAME")
            {
                this.mapShapeLayer.LabelAttributeName = "ISO_3DIGIT";
            }
            else
            {
                this.mapShapeLayer.LabelAttributeName = "CNTRY_NAME";
            }
        }
    }
}