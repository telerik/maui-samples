using System;
using Microsoft.Maui.Controls.Xaml;
using Telerik.Maui.Controls;

namespace QSF.Examples.SchedulerControl.FirstLookExample;

[XamlCompilation(XamlCompilationOptions.Compile)]
public partial class FirstLookView : RadContentView
{
    public FirstLookView()
    {
        InitializeComponent();

        this.scheduler.ScrollIntoView(new TimeOnly(9, 0, 0));
    }
}
