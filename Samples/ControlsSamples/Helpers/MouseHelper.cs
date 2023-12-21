using Microsoft.Maui;
using Microsoft.Maui.Controls;
using System;
using System.Collections.Generic;

namespace QSF.Helpers;

internal partial class MouseHelper
{
    internal readonly View view;
    internal readonly List<Action> mouseEnterHandlers;
    internal readonly List<Action> mouseExitHandlers;

    private IViewHandler handler;
    private object platformView;
    private MouseCursorType mouseCursorType;

    internal MouseHelper(View view)
    {
        this.view = view;
        this.mouseEnterHandlers = new List<Action>();
        this.mouseExitHandlers = new List<Action>();

        this.Handler = this.view.Handler;
        this.view.HandlerChanged += this.View_HandlerChanged;
    }

    internal MouseCursorType MouseCursorType
    {
        get
        {
            return this.mouseCursorType;
        }
        set
        {
            if (this.mouseCursorType != value)
            {
                this.mouseCursorType = value;
                this.OnMouseCursorTypeChanged();
            }
        }
    }

    private IViewHandler Handler
    {
        get
        {
            return this.handler;
        }
        set
        {
            if (this.handler != value)
            {
                this.handler = value;
                this.OnHandlerChanged();
            }
        }
    }

    private object PlatformView
    {
        get
        {
            return this.platformView;
        }
        set
        {
            if (this.platformView != value)
            {
                object oldPlatformView = this.platformView;
                this.platformView = value;
                this.OnPlatformViewChanged(oldPlatformView);
            }
        }
    }

    private void OnHandlerChanged()
    {
        this.PlatformView = this.handler?.PlatformView;
    }

    private void OnPlatformViewChanged(object oldValue)
    {
#if WINDOWS
        this.HandlePlatformViewChanged(oldValue);
#endif
    }

    private void OnMouseCursorTypeChanged()
    {
#if WINDOWS || MACCATALYST
        this.HandleMouseCursorTypeChanged();
#endif
    }

    private void View_HandlerChanged(object sender, EventArgs e)
    {
        this.Handler = this.view.Handler;
    }

    private void Raise(List<Action> handlers)
    {
        foreach (Action action in handlers)
        {
            action?.Invoke();
        }
    }
}
