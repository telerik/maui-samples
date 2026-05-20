#if WINDOWS
using System.Runtime.InteropServices;
using Microsoft.Maui.Controls;
using Telerik.Maui.Controls;
using Telerik.Maui.Controls.RichTextEditor;
using Windows.Graphics.Imaging;
using Application = Microsoft.Maui.Controls.Application;

namespace Telerik.AppUtils.Services;

/// <summary>
/// Handles CAPTURE_WINDOW commands from the test TCP server.
/// Captures the full app window using Win32 PrintWindow (PW_RENDERFULLCONTENT), which captures
/// DirectComposition surfaces including WebView2 content — without involving WinAppDriver/UIA,
/// so it avoids the 60-second hang that occurs when WinAppDriver traverses the WebView2 UIA tree.
///
/// Command format: CAPTURE_WINDOW:{expectedHtmlText}
///   - expectedHtmlText: optional. If provided, polls the RadRichTextEditor until the HTML
///     contains this text before taking the screenshot.
///
/// Response format: OK|width|height|/path/to/capture.png
///
/// Active only when registered (inside IsAppUnderTest guard).
/// </summary>
internal static class CaptureWindowHandler
{
    internal static void Register(TestingService service)
    {
        service.OnCommand += HandleCommand;
    }

    private static void HandleCommand(object? sender, TestCommandEventArgs e)
    {
        if (!e.Command.StartsWith("CAPTURE_WINDOW:", StringComparison.OrdinalIgnoreCase))
        {
            return;
        }

        var tcs = new TaskCompletionSource<string>();
        e.Result = tcs.Task;

        Application.Current!.Dispatcher.Dispatch(async () =>
        {
            try
            {
                string expectedText = e.Command["CAPTURE_WINDOW:".Length..].Trim();

                if (!string.IsNullOrEmpty(expectedText))
                {
                    await WaitForRteContentAsync(expectedText);
                }

                var captureResult = await CaptureWindowToPngAsync();
                tcs.SetResult($"OK|{captureResult}");
            }
            catch (Exception ex)
            {
                tcs.SetException(ex);
            }
        });
    }

    private static async Task WaitForRteContentAsync(string expectedText)
    {
        var richTextEditor = FindRichTextEditorInCurrentPage();
        if (richTextEditor == null)
        {
            return;
        }

        for (int attempt = 0; attempt < 150; attempt++)
        {
            try
            {
                var html = await richTextEditor.GetHtmlAsync();
                if (!string.IsNullOrWhiteSpace(html) && html.Contains(expectedText, StringComparison.OrdinalIgnoreCase))
                {
                    return;
                }
            }
            catch
            {
                // Transient error during initialization — retry
            }

            await Task.Delay(200);
        }
    }

    private static async Task<string> CaptureWindowToPngAsync()
    {
        var hwnd = GetAppHwnd();
        if (hwnd == IntPtr.Zero)
        {
            throw new InvalidOperationException("Could not find the app window HWND.");
        }

        if (!GetWindowRect(hwnd, out RECT rect))
        {
            throw new InvalidOperationException("GetWindowRect failed.");
        }

        int width = rect.Right - rect.Left;
        int height = rect.Bottom - rect.Top;

        // Capture window content using PrintWindow with PW_RENDERFULLCONTENT.
        // This flag captures DirectComposition surfaces (including WebView2) that are
        // otherwise invisible to plain BitBlt from the desktop DC.
        int stride = width * 4;
        byte[] pixels = new byte[stride * height];

        IntPtr screenDc = GetDC(IntPtr.Zero);
        IntPtr memDc = CreateCompatibleDC(screenDc);
        IntPtr hBitmap = CreateCompatibleBitmap(screenDc, width, height);
        IntPtr oldObj = SelectObject(memDc, hBitmap);

        try
        {
            PrintWindow(hwnd, memDc, PW_RENDERFULLCONTENT);

            var bmi = new BITMAPINFO();
            bmi.bmiHeader.biSize = Marshal.SizeOf<BITMAPINFOHEADER>();
            bmi.bmiHeader.biWidth = width;
            bmi.bmiHeader.biHeight = -height; // negative = top-down scanlines
            bmi.bmiHeader.biPlanes = 1;
            bmi.bmiHeader.biBitCount = 32;
            bmi.bmiHeader.biCompression = 0; // BI_RGB

            GetDIBits(memDc, hBitmap, 0, (uint)height, pixels, ref bmi, 0);
        }
        finally
        {
            SelectObject(memDc, oldObj);
            DeleteObject(hBitmap);
            DeleteDC(memDc);
            ReleaseDC(IntPtr.Zero, screenDc);
        }

        // GDI returns BGRX (alpha = 0); set alpha = 255 before encoding.
        for (int i = 3; i < pixels.Length; i += 4)
        {
            pixels[i] = 255;
        }

        // Encode to PNG using WinRT BitmapEncoder (no external NuGet needed).
        var screenshotPath = Path.Combine(Path.GetTempPath(), $"capture-window-{Guid.NewGuid():N}.png");
        using var fileStream = File.Create(screenshotPath);
        using var randomAccessStream = fileStream.AsRandomAccessStream();
        var encoder = await BitmapEncoder.CreateAsync(BitmapEncoder.PngEncoderId, randomAccessStream);
        encoder.SetPixelData(
            BitmapPixelFormat.Bgra8,
            BitmapAlphaMode.Ignore,
            (uint)width,
            (uint)height,
            96, 96,
            pixels);
        await encoder.FlushAsync();

        return $"{width}|{height}|{screenshotPath}";
    }

    private static IntPtr GetAppHwnd()
    {
        if (Application.Current?.Windows.Count > 0)
        {
            var platformView = Application.Current.Windows[0].Handler?.PlatformView;
            if (platformView is Microsoft.UI.Xaml.Window winUiWindow)
            {
                return WinRT.Interop.WindowNative.GetWindowHandle(winUiWindow);
            }
        }

        return IntPtr.Zero;
    }

    private static RadRichTextEditor? FindRichTextEditorInCurrentPage()
    {
        if (Application.Current?.Windows.Count == 0)
        {
            return null;
        }

        var page = Application.Current!.Windows[0].Page;
        if (page is NavigationPage navigationPage)
        {
            page = navigationPage.CurrentPage;
        }

        return ChildOfType<RadRichTextEditor>(page!);
    }

    private static T? ChildOfType<T>(VisualElement visualElement)
        where T : VisualElement
    {
        if (visualElement == null)
        {
            return null;
        }

        foreach (var item in VisualTreeElementExtensions.GetVisualTreeDescendants(visualElement))
        {
            if (item is T targetElement)
            {
                return targetElement;
            }
        }

        return null;
    }

    // Win32 P/Invokes
    private const uint PW_RENDERFULLCONTENT = 0x2;

    [DllImport("user32.dll")]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static extern bool PrintWindow(IntPtr hwnd, IntPtr hdcBlt, uint nFlags);

    [DllImport("user32.dll")]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static extern bool GetWindowRect(IntPtr hwnd, out RECT lpRect);

    [DllImport("user32.dll")]
    private static extern IntPtr GetDC(IntPtr hwnd);

    [DllImport("user32.dll")]
    private static extern int ReleaseDC(IntPtr hwnd, IntPtr hdc);

    [DllImport("gdi32.dll")]
    private static extern IntPtr CreateCompatibleDC(IntPtr hdc);

    [DllImport("gdi32.dll")]
    private static extern IntPtr CreateCompatibleBitmap(IntPtr hdc, int cx, int cy);

    [DllImport("gdi32.dll")]
    private static extern IntPtr SelectObject(IntPtr hdc, IntPtr h);

    [DllImport("gdi32.dll")]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static extern bool DeleteObject(IntPtr ho);

    [DllImport("gdi32.dll")]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static extern bool DeleteDC(IntPtr hdc);

    [DllImport("gdi32.dll")]
    private static extern int GetDIBits(IntPtr hdc, IntPtr hbmp, uint start, uint cLines,
        byte[] lpvBits, ref BITMAPINFO lpbmi, uint usage);

    [StructLayout(LayoutKind.Sequential)]
    private struct RECT
    {
        public int Left, Top, Right, Bottom;
    }

    [StructLayout(LayoutKind.Sequential)]
    private struct BITMAPINFOHEADER
    {
        public int biSize, biWidth, biHeight;
        public short biPlanes, biBitCount;
        public int biCompression, biSizeImage, biXPelsPerMeter, biYPelsPerMeter, biClrUsed, biClrImportant;
    }

    [StructLayout(LayoutKind.Sequential)]
    private struct BITMAPINFO
    {
        public BITMAPINFOHEADER bmiHeader;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
        public uint[]? bmiColors;
    }
}
#endif
