using Microsoft.Maui.Controls.Xaml;
using Telerik.Maui.Controls;

namespace QSF.Examples.SchedulerControl.ThemingExample;

[XamlCompilation(XamlCompilationOptions.Compile)]
public partial class ThemingView : RadContentView
{
    public ThemingView()
    {
        InitializeComponent();
    }

    private void RadScheduler_DialogOpening(object sender, Telerik.Maui.Controls.Scheduler.SchedulerDialogOpeningEventArgs e)
    {
#if ANDROID || IOS
        e.Cancel = true;
#endif
    }
}
