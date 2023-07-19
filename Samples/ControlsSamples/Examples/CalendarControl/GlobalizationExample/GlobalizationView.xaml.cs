using System;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Controls.Xaml;

namespace QSF.Examples.CalendarControl.GlobalizationExample;

[XamlCompilation(XamlCompilationOptions.Compile)]
public partial class GlobalizationView : ContentView
{
    public GlobalizationView()
    {
        InitializeComponent();

        this.calendar.SelectedDate = DateTime.Today;
    }
}