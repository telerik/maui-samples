using System;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Controls.Xaml;

namespace QSF.Examples.CalendarControl.ConfigurationExample;

[XamlCompilation(XamlCompilationOptions.Compile)]
public partial class ConfigurationView : ContentView
{
    public ConfigurationView()
    {
        InitializeComponent();

        this.calendar.SelectedDate = DateTime.Today;
    }
}