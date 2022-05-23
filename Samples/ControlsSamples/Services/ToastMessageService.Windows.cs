#if WINDOWS
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using System;

namespace QSF.Services;

partial class ToastMessageService : IToastMessageService
{
    public void ShortAlert(string message)
    {
        ShowMessage(message);
    }

    private void ShowMessage(string message)
    {
        var label = new TextBlock
        {
            Text = message,
            HorizontalAlignment = HorizontalAlignment.Center,
            VerticalAlignment = VerticalAlignment.Center,
        };

        var flyout = new Flyout
        {
            Content = label,
            Placement = Microsoft.UI.Xaml.Controls.Primitives.FlyoutPlacementMode.Bottom
        };

        flyout.ShowAt((FrameworkElement)Microsoft.Maui.Controls.Application.Current.MainPage.Handler.PlatformView);

        var timer = new DispatcherTimer { Interval = TimeSpan.FromSeconds(3.5) };
        timer.Tick += (sender, e) =>
        {
            timer.Stop();
            flyout.Hide();
        };
        timer.Start();
    }
}
#endif