using Microsoft.Maui.Controls.Xaml;
using System;
using Telerik.Maui.Controls;
using Telerik.Maui.Controls.Compatibility.Map;

namespace SDKBrowserMaui.Examples.MapControl.FeaturesCategory.InteractionModeExample
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class InteractionMode : RadContentView
    {
        private Array modes = Enum.GetValues(typeof(Telerik.Maui.Controls.Compatibility.Map.InteractionMode));

        public InteractionMode()
        {
            InitializeComponent();

            // >> map-interactionmode-settintsource
            var assembly = this.GetType().Assembly;
            var source = MapSource.FromResource("SDKBrowserMaui.Examples.MapControl.world.shp", assembly);
            this.reader.Source = source;
            // << map-interactionmode-settintsource

            this.interactionModeSegmented.ItemsSource = this.modes;
            this.interactionModeSegmented.SelectedIndex = Array.IndexOf(this.modes, this.map.InteractionMode);
        }

        private void InteractionModeChanged(object sender, EventArgs e)
        {
            this.map.InteractionMode = (Telerik.Maui.Controls.Compatibility.Map.InteractionMode)this.modes.GetValue(this.interactionModeSegmented.SelectedIndex);
        }
    }
}