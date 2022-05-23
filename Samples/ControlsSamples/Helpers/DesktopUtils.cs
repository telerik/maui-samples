using Microsoft.Maui.Controls;
using Microsoft.Maui.Devices;
using System;

namespace QSF.Helpers;

public static class DesktopUtils
{
    public static readonly BindableProperty MouseCursorTypeProperty = BindableProperty.CreateAttached(
        nameof(MouseCursorType), typeof(MouseCursorType), typeof(DesktopUtils), MouseCursorType.Arrow,
        propertyChanged: OnMouseCursorTypeChanged);

    private static readonly BindableProperty MouseHelperProperty = BindableProperty.CreateAttached(
        nameof(MouseHelper), typeof(MouseHelper), typeof(DesktopUtils), null);

    public static MouseCursorType GetMouseCursorType(BindableObject bindable)
    {
        return (MouseCursorType)bindable.GetValue(MouseCursorTypeProperty);
    }

    public static void SetMouseCursorType(BindableObject bindable, MouseCursorType value)
    {
        bindable.SetValue(MouseCursorTypeProperty, value);
    }

    public static void AttachMouseEnterHandler(View view, Action handler)
    {
        if (DeviceInfo.Idiom == DeviceIdiom.Desktop)
        {
            GetOrCreateMouseHelper(view).mouseEnterHandlers.Add(handler);
        }
    }

    private static MouseHelper GetMouseHelper(BindableObject bindable)
    {
        return (MouseHelper)bindable.GetValue(MouseHelperProperty);
    }

    private static void SetMouseHelper(BindableObject bindable, MouseHelper value)
    {
        bindable.SetValue(MouseHelperProperty, value);
    }

    private static MouseHelper GetOrCreateMouseHelper(View view)
    {
        MouseHelper mouseHelper = GetMouseHelper(view);

        if (mouseHelper == null)
        {
            mouseHelper = new MouseHelper(view);
            SetMouseHelper(view, mouseHelper);
        }

        return mouseHelper;
    }

    private static void OnMouseCursorTypeChanged(BindableObject bindable, object oldValue, object newValue)
    {
        if (bindable is View view)
        {
            GetOrCreateMouseHelper(view).MouseCursorType = (MouseCursorType)newValue;
        }
    }
}
