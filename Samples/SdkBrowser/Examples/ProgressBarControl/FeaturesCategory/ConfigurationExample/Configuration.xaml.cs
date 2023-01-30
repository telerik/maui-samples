using Microsoft.Maui.Controls;
using System;

namespace SDKBrowserMaui.Examples.ProgressBarControl.FeaturesCategory.ConfigurationExample;

public partial class Configuration : ContentView
{
    public Configuration()
    {
        InitializeComponent();
    }

    // >> progressbar-configuration-valueupdated
    private void ProgressBarUpdateClicked(object sender, EventArgs e)
    {
        this.progressBar.Value = 75;
        this.updateButton.IsEnabled = false;
    }
    // << progressbar-configuration-valueupdated
}
