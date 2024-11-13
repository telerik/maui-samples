using Microsoft.Maui.Controls;
using System;
using static Telerik.Maui.Controls.RadGridSplitter;

namespace SDKBrowserMaui.Examples.GridSplitterControl.FeaturesCategory.ResizeConfigurationExample;

public partial class ResizeConfiguration : ContentView
{
    public ResizeConfiguration()
    {
        InitializeComponent();

        this.comboRB.ItemsSource = Enum.GetValues(typeof(GridResizeBehavior));
    }
}