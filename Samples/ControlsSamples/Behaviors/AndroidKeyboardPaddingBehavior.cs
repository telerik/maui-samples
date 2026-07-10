using Microsoft.Maui;
using Microsoft.Maui.Controls;
using System;
#if ANDROID
using Microsoft.Maui.Platform;
#endif

namespace QSF.Behaviors;

/// <summary>
/// On Android, monitors soft-keyboard visibility via window insets and adjusts
/// the attached layout's bottom <see cref="Layout.Padding"/> so that all content
/// remains above the keyboard. The MAUI layout system re-measures automatically:
/// a Grid's star-sized rows shrink while Auto rows (toolbar, status bar, etc.)
/// keep their natural size.
/// On other platforms the behavior is a no-op.
/// </summary>
public class AndroidKeyboardPaddingBehavior : Behavior<Layout>
{
#if ANDROID
    private Layout layout;
    private Thickness originalPadding;
    private InsetObserver observer;
    private global::Android.Views.View rootView;
    private readonly System.Collections.Generic.List<BindableObject> savedTranslationTargets = new();
    private readonly System.Collections.Generic.List<BindableObject> savedTranslationPivots = new();

    protected override void OnAttachedTo(Layout layout)
    {
        base.OnAttachedTo(layout);
        this.layout = layout;
        this.originalPadding = layout.Padding;
        layout.Loaded += this.OnLoaded;
        layout.Unloaded += this.OnUnloaded;
    }

    protected override void OnDetachingFrom(Layout layout)
    {
        layout.Loaded -= this.OnLoaded;
        layout.Unloaded -= this.OnUnloaded;
        this.RemoveObserver();
        layout.Padding = this.originalPadding;
        this.layout = null;
        base.OnDetachingFrom(layout);
    }

    private void OnLoaded(object sender, EventArgs e)
    {
        this.DisableInnerTranslations();
        this.AttachObserver();
    }

    private void OnUnloaded(object sender, EventArgs e)
    {
        this.RemoveObserver();
        this.RestoreInnerTranslations();

        if (this.layout != null)
        {
            this.layout.Padding = this.originalPadding;
        }
    }

    // Scans direct children of the layout and disables any KeyboardHelper translation
    // properties so they don't conflict with the padding-based keyboard avoidance.
    private void DisableInnerTranslations()
    {
        if (this.layout == null)
        {
            return;
        }

        foreach (var child in this.layout.Children)
        {
            if (child is not BindableObject bindable)
            {
                continue;
            }

            if (Telerik.Maui.Controls.KeyboardHelper.GetIsTranslationTarget(bindable))
            {
                this.savedTranslationTargets.Add(bindable);
                Telerik.Maui.Controls.KeyboardHelper.SetIsTranslationTarget(bindable, false);
            }

            if (Telerik.Maui.Controls.KeyboardHelper.GetIsTranslationPivot(bindable))
            {
                this.savedTranslationPivots.Add(bindable);
                Telerik.Maui.Controls.KeyboardHelper.SetIsTranslationPivot(bindable, false);
            }
        }
    }

    private void RestoreInnerTranslations()
    {
        foreach (var bindable in this.savedTranslationTargets)
        {
            Telerik.Maui.Controls.KeyboardHelper.SetIsTranslationTarget(bindable, true);
        }

        foreach (var bindable in this.savedTranslationPivots)
        {
            Telerik.Maui.Controls.KeyboardHelper.SetIsTranslationPivot(bindable, true);
        }

        this.savedTranslationTargets.Clear();
        this.savedTranslationPivots.Clear();
    }

    private void AttachObserver()
    {
        this.RemoveObserver();

        var activity = Microsoft.Maui.ApplicationModel.Platform.CurrentActivity;
        this.rootView = activity?.Window?.DecorView?.RootView;
        var viewTreeObserver = this.rootView?.ViewTreeObserver;

        if (viewTreeObserver == null || !viewTreeObserver.IsAlive)
        {
            return;
        }

        this.observer = new InsetObserver(this.rootView, this.OnKeyboardHeightChanged);
        viewTreeObserver.AddOnGlobalLayoutListener(this.observer);
    }

    private void RemoveObserver()
    {
        if (this.observer == null)
        {
            return;
        }

        var viewTreeObserver = this.rootView?.ViewTreeObserver;
        if (viewTreeObserver != null && viewTreeObserver.IsAlive)
        {
            viewTreeObserver.RemoveOnGlobalLayoutListener(this.observer);
        }

        this.observer.Dispose();
        this.observer = null;
        this.rootView = null;
    }

    private void OnKeyboardHeightChanged(double keyboardHeight)
    {
        if (this.layout == null)
        {
            return;
        }

        this.layout.Dispatcher.Dispatch(() =>
        {
            if (this.layout == null)
            {
                return;
            }

            this.layout.Padding = new Thickness(
                this.originalPadding.Left,
                this.originalPadding.Top,
                this.originalPadding.Right,
                this.originalPadding.Bottom + keyboardHeight);
        });
    }

    private sealed class InsetObserver : Java.Lang.Object, global::Android.Views.ViewTreeObserver.IOnGlobalLayoutListener
    {
        private readonly global::Android.Views.View rootView;
        private readonly Action<double> callback;
        private double lastHeight;

        public InsetObserver(global::Android.Views.View rootView, Action<double> callback)
        {
            this.rootView = rootView;
            this.callback = callback;
        }

        public void OnGlobalLayout()
        {
            var insets = AndroidX.Core.View.ViewCompat.GetRootWindowInsets(this.rootView);
            if (insets == null)
            {
                return;
            }

            double keyboardHeight = 0;

            if (insets.IsVisible(AndroidX.Core.View.WindowInsetsCompat.Type.Ime()))
            {
                var imeInsets = insets.GetInsets(AndroidX.Core.View.WindowInsetsCompat.Type.Ime());
                var navInsets = insets.GetInsets(AndroidX.Core.View.WindowInsetsCompat.Type.NavigationBars());

                // IME bottom includes the navigation-bar area; subtract it so we get
                // only the keyboard height above the nav bar (the MAUI content area
                // already stops at the nav bar).
                int effectivePixels = Math.Max(0, imeInsets.Bottom - navInsets.Bottom);
                keyboardHeight = this.rootView.Context.FromPixels(effectivePixels);
            }

            if (Math.Abs(keyboardHeight - this.lastHeight) > 1)
            {
                this.lastHeight = keyboardHeight;
                this.callback(keyboardHeight);
            }
        }
    }
#endif
}
