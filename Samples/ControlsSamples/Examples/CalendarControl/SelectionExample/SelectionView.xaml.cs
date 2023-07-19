using System;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Controls.Xaml;

namespace QSF.Examples.CalendarControl.SelectionExample;

[XamlCompilation(XamlCompilationOptions.Compile)]
public partial class SelectionView : ContentView
{
    public SelectionView()
    {
        InitializeComponent();

        this.calendar.SelectedDate = DateTime.Today;
    }
}