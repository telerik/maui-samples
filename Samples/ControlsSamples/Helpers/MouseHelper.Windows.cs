#if WINDOWS
using System;
using System.Reflection;
using W = Microsoft.UI.Xaml;
using WI = Microsoft.UI.Input;

namespace QSF.Helpers;

partial class MouseHelper
{
    private static WI.InputCursor ToInputCursor(MouseCursorType mouseCursorType)
    {
        WI.InputSystemCursorShape cursorShape = ToInputSystemCursorShape(mouseCursorType);
        WI.InputSystemCursor coreCursor = WI.InputSystemCursor.Create(cursorShape);
        return coreCursor;
    }

    private static WI.InputSystemCursorShape ToInputSystemCursorShape(MouseCursorType mouseCursorType)
    {
        switch (mouseCursorType)
        {
            case MouseCursorType.Arrow:
                return WI.InputSystemCursorShape.Arrow;
            case MouseCursorType.Hand:
                return WI.InputSystemCursorShape.Hand;
            case MouseCursorType.IBeam:
                return WI.InputSystemCursorShape.IBeam;
            default:
                throw new InvalidOperationException();
        }
    }

    private void HandlePlatformViewChanged(object oldValue)
    {
        W.UIElement uiElement = oldValue as W.UIElement;
        if (uiElement != null)
        {
            uiElement.PointerEntered -= this.uiElement_PointerEntered;
            uiElement.PointerExited -= this.uiElement_PointerExited;
        }

        uiElement = this.platformView as W.UIElement;
        if (uiElement != null)
        {
            uiElement.PointerEntered += this.uiElement_PointerEntered;
            uiElement.PointerExited += this.uiElement_PointerExited;
        }
    }

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

    private void UpdatePlatformCursor()
    {
        W.UIElement uiElement = this.platformView as W.UIElement;
        if (uiElement != null)
        {
            Type type = uiElement.GetType();
            PropertyInfo protectedCursorProperty = type.GetProperty("ProtectedCursor", BindingFlags.Instance | BindingFlags.NonPublic);
            WI.InputCursor cursor = ToInputCursor(this.mouseCursorType);

            try
            {
                protectedCursorProperty.SetValue(uiElement, cursor);
            }
            catch { }
        }
    }

    private void uiElement_PointerEntered(object sender, W.Input.PointerRoutedEventArgs e)
    {
        this.Raise(this.mouseEnterHandlers);
    }

    private void uiElement_PointerExited(object sender, W.Input.PointerRoutedEventArgs e)
    {
        this.Raise(this.mouseExitHandlers);
    }

    private void View_Loaded(object sender, EventArgs e)
    {
        this.view.Loaded -= this.View_Loaded;
        this.UpdatePlatformCursor();
    }
}
#endif