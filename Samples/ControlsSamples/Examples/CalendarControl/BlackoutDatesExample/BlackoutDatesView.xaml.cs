using System;
using System.Collections.Generic;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Controls.Xaml;

namespace QSF.Examples.CalendarControl.BlackoutDatesExample;

[XamlCompilation(XamlCompilationOptions.Compile)]
public partial class BlackoutDatesView : ContentView
{
    public BlackoutDatesView()
    {
        InitializeComponent();

        int currentYear = DateTime.Now.Year;

        List<DateTime> blackoutDates = new List<DateTime>();
        blackoutDates.Add(new DateTime(currentYear, 3, 5));
        blackoutDates.Add(new DateTime(currentYear, 3, 8));
        blackoutDates.Add(new DateTime(currentYear, 3, 10));
        blackoutDates.Add(new DateTime(currentYear, 3, 18));
        blackoutDates.Add(new DateTime(currentYear, 3, 19));

        this.calendar.DisplayDate = new DateTime(currentYear, 3, 1);
        this.calendar.BlackoutDates = blackoutDates;
    }
}