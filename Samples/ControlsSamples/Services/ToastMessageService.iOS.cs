#if IOS || MACCATALYST 
using CoreGraphics;
using Foundation;
using UIKit;

namespace QSF.Services;

partial class ToastMessageService 
{
    private NSTimer alertDelay;
    private UIAlertController alert;

    public void ShortAlert(string message)
    {
        this.ShowAlert(message, 3.5);
    }

    private void ShowAlert(string message, double seconds)
    {
        this.alertDelay = NSTimer.CreateScheduledTimer(seconds, (obj) =>
        {
            this.DismissMessage();
        });

        this.alert = UIAlertController.Create(null, message, UIAlertControllerStyle.ActionSheet);

        var rootViewController = UIApplication.SharedApplication.KeyWindow.RootViewController;
        var popoverPresentationController = this.alert.PopoverPresentationController;

        if (popoverPresentationController != null)
        {
            var rootView = rootViewController.View;
            var rootRect = rootViewController.View.Bounds;

#if MACCATALYST
            var sourceRect = new CGRect(0, rootRect.Height - 10, rootRect.Width, 10);
            var arrowDirection = UIPopoverArrowDirection.Down;
#else
            var sourceRect = new CGRect(0, rootRect.Height, rootRect.Width, 0);
            var arrowDirection = default(UIPopoverArrowDirection);
#endif

            popoverPresentationController.SourceView = rootView;
            popoverPresentationController.SourceRect = sourceRect;
            popoverPresentationController.PermittedArrowDirections = arrowDirection;
        }

        rootViewController.PresentViewController(this.alert, true, null);
    }

    private void DismissMessage()
    {
        if (this.alert != null)
        {
            this.alert.DismissViewController(true, null);
        }
        if (this.alertDelay != null)
        {
            this.alertDelay.Dispose();
        }
    }
}
#endif