using System;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Controls.Xaml;

namespace QSF.Examples.CalendarControl.DisplayModeExample;

[XamlCompilation(XamlCompilationOptions.Compile)]
public partial class DisplayModeView : ContentView
{
    public DisplayModeView()
    {
        InitializeComponent();

        this.calendar.SelectedDate = DateTime.Today;
    }
}