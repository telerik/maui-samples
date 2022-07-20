﻿using Microsoft.Maui.Controls.Xaml;
using Telerik.Maui.Controls;
using Telerik.Maui.Controls.Compatibility.Map;

namespace SDKBrowserMaui.Examples.MapControl.FeaturesCategory.ShapeLabelStyleExample
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ShapeLabelStyle : RadContentView
    {
        public ShapeLabelStyle()
        {
            InitializeComponent();

            var assembly = this.GetType().Assembly;
            var source = MapSource.FromResource("SDKBrowserMaui.Examples.MapControl.world.shp", assembly);
            var dataSource = MapSource.FromResource("SDKBrowserMaui.Examples.MapControl.world.dbf", assembly);
            this.reader.Source = source;
            this.reader.DataSource = dataSource;
        }
    }
}