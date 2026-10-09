#if MACCATALYST
using AppKit;
using UIKit;

namespace QSF.Helpers;

partial class MouseHelper
{
    private UIHoverGestureRecognizer hoverGesture;

    // NSCursor.Set() is global, not scoped to a view, so it must be driven by native hover state instead of a one-time apply.
    private void HandlePlatformViewChanged(object oldValue)
    {
        if (oldValue is UIView oldNativeView && this.hoverGesture != null)
        {
            oldNativeView.RemoveGestureRecognizer(this.hoverGesture);
            this.hoverGesture = null;
        }

        if (this.platformView is UIView nativeView)
        {
            this.hoverGesture = new UIHoverGestureRecognizer(this.OnHoverGestureRecognized);
            nativeView.AddGestureRecognizer(this.hoverGesture);
        }
    }

    private void OnHoverGestureRecognized()
    {
        switch (this.hoverGesture.State)
        {
            case UIGestureRecognizerState.Began:
            case UIGestureRecognizerState.Changed:
                this.UpdatePlatformCursor(this.mouseCursorType);
                break;
            case UIGestureRecognizerState.Ended:
            case UIGestureRecognizerState.Cancelled:
            case UIGestureRecognizerState.Failed:
                this.UpdatePlatformCursor(MouseCursorType.Arrow);
                break;
        }
    }

    private void UpdatePlatformCursor(MouseCursorType cursorType)
    {
        switch (cursorType)
        {
            case MouseCursorType.Arrow:
                NSCursor.ArrowCursor.Set();
                break;
            case MouseCursorType.Hand:
                NSCursor.PointingHandCursor.Set();
                break;
            case MouseCursorType.IBeam:
                NSCursor.IBeamCursor.Set();
                break;
            default:
                break;
        }
    }
}
#endif