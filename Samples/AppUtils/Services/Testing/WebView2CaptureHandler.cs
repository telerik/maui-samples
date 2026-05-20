#if WINDOWS
using Microsoft.Maui.Controls;
using Telerik.Maui.Controls;
using Telerik.Maui.Controls.RichTextEditor;
using Application = Microsoft.Maui.Controls.Application;

namespace Telerik.AppUtils.Services;

/// <summary>
/// Handles WEBVIEW2_CAPTURE commands from the test TCP server.
/// Finds a RadRichTextEditor in the current page, waits for its HTML content
/// to contain the expected text, then captures the WebView2 preview as PNG.
/// 
/// Active only when registered (inside IsAppUnderTest guard).
/// </summary>
internal static class WebView2CaptureHandler
{
    internal static void Register(TestingService service)
    {
        service.OnCommand += HandleCommand;
    }

    private static void HandleCommand(object? sender, TestCommandEventArgs e)
    {
        if (!e.Command.StartsWith("WEBVIEW2_CAPTURE:", StringComparison.OrdinalIgnoreCase))
        {
            return;
        }

        var tcs = new TaskCompletionSource<string>();
        e.Result = tcs.Task;

        Application.Current!.Dispatcher.Dispatch(async () =>
        {
            try
            {
                string expectedText = e.Command["WEBVIEW2_CAPTURE:".Length..].Trim();
                var richTextEditor = FindRichTextEditorInCurrentPage();

                if (richTextEditor == null)
                {
                    tcs.SetResult("ERROR|Could not find RadRichTextEditor in the current page.");
                    return;
                }

                string? lastHtml = null;
                string? lastExceptionMessage = null;
                for (int attempt = 0; attempt < 150; attempt++)
                {
                    try
                    {
                        var html = await richTextEditor.GetHtmlAsync();
                        lastHtml = html;
                        if (!string.IsNullOrWhiteSpace(html) && html.Contains(expectedText, StringComparison.OrdinalIgnoreCase))
                        {
                            // HTML contains expected text — attempt capture (don't catch here; let it propagate as a real error)
                            var captureResult = await CaptureWebView2Async(richTextEditor);
                            tcs.SetResult($"OK|{captureResult}");
                            return;
                        }
                    }
                    catch (Exception ex) when (IsTransientRteError(ex))
                    {
                        lastExceptionMessage = ex.GetType().Name + ": " + ex.Message;
                    }

                    await Task.Delay(200);
                }

                // Include diagnostics so we can see what GetHtmlAsync() returned.
                string diagHtml = lastHtml == null ? "(null)" : (lastHtml.Length > 200 ? lastHtml[..200] : lastHtml);
                string diagEx = lastExceptionMessage ?? "(none)";
                tcs.SetResult($"TIMEOUT|html={diagHtml}|ex={diagEx}");
            }
            catch (Exception ex)
            {
                tcs.SetException(ex);
            }
        });
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

    private static async Task<string> CaptureWebView2Async(RadRichTextEditor richTextEditor)
    {
        var screenshotPath = Path.Combine(Path.GetTempPath(), $"webview2-capture-{Guid.NewGuid():N}.png");
        var platformView = richTextEditor.Handler?.PlatformView as Microsoft.UI.Xaml.DependencyObject
            ?? throw new InvalidOperationException("Could not find the RichTextEditor platform view (Handler or PlatformView is null).");
        var webView = (platformView as Microsoft.UI.Xaml.Controls.WebView2)
            ?? FindDescendant<Microsoft.UI.Xaml.Controls.WebView2>(platformView)
            ?? throw new InvalidOperationException($"Could not find the RichTextEditor WebView2 platform view. PlatformView type: {platformView.GetType().FullName}");

        if (webView.CoreWebView2 == null)
        {
            throw new InvalidOperationException("The RichTextEditor WebView2 core is not ready.");
        }

        await using (var fileStream = File.Create(screenshotPath))
        {
            using var randomAccessStream = fileStream.AsRandomAccessStream();
            await webView.CoreWebView2.CapturePreviewAsync(
                Microsoft.Web.WebView2.Core.CoreWebView2CapturePreviewImageFormat.Png,
                randomAccessStream);
        }

        var scale = webView.XamlRoot?.RasterizationScale ?? 1d;
        int width = (int)Math.Round(webView.ActualWidth * scale);
        int height = (int)Math.Round(webView.ActualHeight * scale);

        return $"{width}|{height}|{screenshotPath}";
    }

    /// <summary>
    /// Returns true for errors that indicate the RTE/WebView2 is still initializing and the operation
    /// should be retried.  Returns false for real errors (e.g. WebView2 platform view not found after
    /// content has loaded, capture failures) so they propagate immediately.
    /// </summary>
    private static bool IsTransientRteError(Exception ex)
    {
        // Only swallow exceptions that look like "editor not ready yet" (null reference / JS interop
        // failures during initialization).  InvalidOperationException from CaptureWebView2Async (e.g.
        // "Could not find WebView2 platform view") is a hard failure and should NOT be swallowed.
        if (ex is InvalidOperationException && ex.Message.Contains("WebView2"))
        {
            return false;
        }

        return true; // NullReferenceException, COM exceptions, etc. during GetHtmlAsync polling
    }

    private static T? FindDescendant<T>(Microsoft.UI.Xaml.DependencyObject? root)
        where T : Microsoft.UI.Xaml.DependencyObject
    {
        if (root == null)
        {
            return null;
        }

        if (root is T target)
        {
            return target;
        }

        int childCount = Microsoft.UI.Xaml.Media.VisualTreeHelper.GetChildrenCount(root);
        for (int i = 0; i < childCount; i++)
        {
            var result = FindDescendant<T>(Microsoft.UI.Xaml.Media.VisualTreeHelper.GetChild(root, i));
            if (result != null)
            {
                return result;
            }
        }

        return null;
    }
}
#endif
