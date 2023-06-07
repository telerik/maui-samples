using System.Collections.Generic;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Controls.Xaml;

namespace QSF.Examples.SlideViewControl.ConfigurationExample;

[XamlCompilation(XamlCompilationOptions.Compile)]
public partial class ConfigurationView : ContentView
{
    public ConfigurationView()
    {
        InitializeComponent();

        this.slideView.ItemsSource = new List<string> { "1", "2", "3", "4", "5", "6", "7", "8", "9", "10" };
    }
}