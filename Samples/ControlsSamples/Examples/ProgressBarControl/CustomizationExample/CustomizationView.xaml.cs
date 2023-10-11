using System;
using Microsoft.Maui.Controls.Xaml;
using Telerik.Maui.Controls;

namespace QSF.Examples.ProgressBarControl.CustomizationExample;

[XamlCompilation(XamlCompilationOptions.Compile)]
public partial class CustomizationView : RadContentView
{
    public CustomizationView()
    {
        InitializeComponent();

        this.progressBar.ProgressChanged += (s, e) =>
        {
            if (this.progressBar.Progress == this.progressBar.Maximum)
            {
                this.tick.Scale = 1;
            }
            else if (this.progressBar.Progress == this.progressBar.Minimum)
            {
                this.tick.Scale = 0;
            }
        };
    }
}
