using Microsoft.UI;
using Microsoft.UI.Xaml;
using Windows.UI;

namespace QSF.WinUI
{
    public static class TelerikTitleBarHelper
    {
        public static void SetRequestedTheme(bool isDark)
        {
            if (Microsoft.Maui.Controls.Application.Current?.Windows.Count > 0 &&
                Microsoft.Maui.Controls.Application.Current.Windows[0].Handler?.PlatformView is Window window)
            {
                var frameworkElement = window.ContentAs<FrameworkElement>();
                frameworkElement.RequestedTheme = isDark  ? ElementTheme.Dark : ElementTheme.Light;

                var handle = WinRT.Interop.WindowNative.GetWindowHandle(window);
                var id = Microsoft.UI.Win32Interop.GetWindowIdFromWindow(handle);
                var appWindow = Microsoft.UI.Windowing.AppWindow.GetFromWindowId(id);
                var titleBar = appWindow.TitleBar;

                titleBar.ButtonBackgroundColor = Colors.Transparent;
                titleBar.ButtonHoverBackgroundColor = isDark ? Color.FromArgb(0x1A, 0xFF, 0xFF, 0xFF) : Color.FromArgb(0x1A, 0x00, 0x00, 0x00);
                titleBar.ButtonInactiveBackgroundColor = Colors.Transparent;
                titleBar.ButtonForegroundColor = isDark ? Colors.White : Colors.Black;
                titleBar.ButtonHoverForegroundColor = isDark ? Colors.White : Colors.Black;
                titleBar.ButtonInactiveForegroundColor = isDark ? Color.FromArgb(0xFF, 0xCC, 0xCC, 0xCC) : Color.FromArgb(0xFF, 0x76, 0x76, 0x76);
            }
        }

        private static T ContentAs<T>(this Window window) where T : class =>
            window.Content as T;
    }
}
