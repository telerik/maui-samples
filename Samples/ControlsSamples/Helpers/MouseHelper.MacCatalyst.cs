#if MACCATALYST
using AppKit;
using System;

namespace QSF.Helpers;

partial class MouseHelper
{
    private void HandleMouseCursorTypeChanged()
    {
        if (this.view.IsLoaded)
        {
            this.UpdatePlatformCursor();
        }
        else
        {
            this.view.Loaded += this.View_Loaded;
        }
    }

    private void View_Loaded(object sender, EventArgs e)
    {
        this.view.Loaded -= this.View_Loaded;
        this.UpdatePlatformCursor();
    }

    private void UpdatePlatformCursor()
    {
        switch(this.mouseCursorType)
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