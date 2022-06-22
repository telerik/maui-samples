using Microsoft.Maui.Controls.Xaml;
using System;
using System.Collections.Generic;
using Telerik.Maui.Controls;
using Telerik.Maui.Controls.Compatibility.Map;
using Telerik.Maui.Controls.Compatibility.ShapefileReader;

namespace SDKBrowserMaui.Examples.MapControl.SelectionCategory.ProgrammaticSelectionExample
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ProgrammaticSelection : RadContentView
    {
        private List<Telerik.Maui.Controls.Compatibility.Map.SelectionMode> modes;

        public ProgrammaticSelection()
        {
            InitializeComponent();

            // >> map-selection-settintsource
            var assembly = this.GetType().Assembly;
            var source = MapSource.FromResource("SDKBrowserMaui.Examples.MapControl.world.shp", assembly);
            var dataSource = MapSource.FromResource("SDKBrowserMaui.Examples.MapControl.world.dbf", assembly);
            this.reader.Source = source;
            this.reader.DataSource = dataSource;
            // << map-selection-settintsource

            this.modes = new List<SelectionMode>()
            {
                SelectionMode.None,
                SelectionMode.Single,
                SelectionMode.Multiple
            };

            this.smSegmented.ItemsSource = this.modes;
            this.smSegmented.SelectedIndex = this.modes.IndexOf(this.mapShapeLayer.SelectionMode);
        }

        private void SelectClicked(object sender, EventArgs e)
        {
            var shape = this.GetItemFromCountryName(this.cntrSelectEntry.Text);
            if (shape != null)
            {
                this.mapShapeLayer.SelectedShapes.Add(shape);
            }

            this.cntrSelectEntry.Text = string.Empty;
        }

        private void DeselectClicked(object sender, EventArgs e)
        {
            var shape = this.GetItemFromCountryName(this.cntrDeselectEntry.Text);
            if (shape != null && this.mapShapeLayer.SelectedShapes.Contains(shape))
            {
                this.mapShapeLayer.SelectedShapes.Remove(shape);
            }

            this.cntrDeselectEntry.Text = string.Empty;
        }

        // >> map-selection-runtime-code
        private void SelectFranceClicked(object sender, EventArgs e)
        {
            var shape = this.GetItemFromCountryName("France");
            if (shape != null)
            {
                this.mapShapeLayer.SelectedShapes.Add(shape);
            }
        }

        private void DeselectFranceClicked(object sender, EventArgs e)
        {
            var shape = this.GetItemFromCountryName("France");
            if (shape != null && this.mapShapeLayer.SelectedShapes.Contains(shape))
            {
                this.mapShapeLayer.SelectedShapes.Remove(shape);
            }
        }

        private IShape GetItemFromCountryName(string countryName)
        {
            foreach (var shape in this.reader.Shapes)
            {
                var name = shape.GetAttribute("CNTRY_NAME").ToString();
                if (name == countryName)
                {
                    return shape;
                }
            }
            return null;
        }
        // << map-selection-runtime-code

        private void DeselectAllClicked(object sender, EventArgs e)
        {
            this.mapShapeLayer.SelectedShapes.Clear();
        }

        private void SelectedModeChanged(object sender, EventArgs e)
        {
            mapShapeLayer.SelectionMode = this.modes[smSegmented.SelectedIndex];
        }
    }
}
