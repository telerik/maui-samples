#if IOS || MACCATALYST
using Foundation;
using QuickLook;
using UIKit;

namespace TelerikCRM.Maui.Services;

public partial class FileViewerService
{
    public Task<bool> View(Stream stream, string filename)
    {
        try
        {
            string path = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            string filePath = Path.Combine(path, filename);

            using (FileStream fileStream = File.Open(filePath, FileMode.Create))
            {
                stream.Seek(0, SeekOrigin.Begin);
                stream.CopyTo(fileStream);
            }

#pragma warning disable CA1422
            UIViewController currentController = UIApplication.SharedApplication.KeyWindow?.RootViewController;
#pragma warning restore CA1422

            while (currentController?.PresentedViewController != null)
            {
                currentController = currentController.PresentedViewController;
            }

            UIView currentView = currentController?.View;
            QLPreviewController qlPreview = new QLPreviewController();
            QLPreviewItem item = new QLPreviewItemBundle(filename, filePath);
            qlPreview.DataSource = new PreviewControllerDS(item);
            currentController?.PresentViewController(qlPreview, true, null);

            return Task.FromResult(true);
        }
        catch
        {
            return Task.FromResult(false);
        }
    }
}

public class QLPreviewItemBundle : QLPreviewItem
{
    private readonly string filePath;

    public QLPreviewItemBundle(string fileName, string filePath)
    {
        this.PreviewItemTitle = fileName;
        this.filePath = filePath;
    }

    public override string PreviewItemTitle { get; }

    public override NSUrl PreviewItemUrl
    {
        get
        {
            var documents = NSBundle.MainBundle.BundlePath;
            var lib = Path.Combine(documents, this.filePath);
            var url = NSUrl.FromFilename(lib);
            return url;
        }
    }
}

public class PreviewControllerDS(QLPreviewItem item) : QLPreviewControllerDataSource
{
    public override IQLPreviewItem GetPreviewItem(QLPreviewController controller, nint index)
    {
        return item;
    }

    public override nint PreviewItemCount(QLPreviewController controller)
    {
        return 1;
    }
}
#endif
