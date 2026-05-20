using System;
using Microsoft.Maui.Controls;

namespace QSF.Behaviors;

/// <summary>
/// On Android API 35+, edge-to-edge is forced and AdjustResize no longer works.
/// This behavior temporarily opts out of edge-to-edge and sets AdjustResize so the
/// system properly resizes the window when the keyboard appears, allowing WebView
/// content to scroll to the cursor.
/// </summary>
    public class AndroidResizeSoftInputModeBehavior : Behavior<Element>
    {
#if ANDROID
        private global::Android.Views.SoftInput initialSoftInputMode = global::Android.Views.SoftInput.AdjustUnspecified;
        private bool hasCapturedInitialState;
#endif

        protected override void OnAttachedTo(Element element)
        {
            base.OnAttachedTo(element);

#if ANDROID
            element.ParentChanged += this.OnParentChanged;
            this.ApplyResizeMode();
#endif
        }

        protected override void OnDetachingFrom(Element element)
        {
            base.OnDetachingFrom(element);

#if ANDROID
            element.ParentChanged -= this.OnParentChanged;
            this.RestoreOriginalMode();
#endif
        }

#if ANDROID
        private void OnParentChanged(object sender, EventArgs e)
        {
            if (sender is Element element)
            {
                if (element.Parent != null)
                {
                    this.ApplyResizeMode();
                }
                else
                {
                    this.RestoreOriginalMode();
                }
            }
        }

        private void ApplyResizeMode()
        {
            var activity = Microsoft.Maui.ApplicationModel.Platform.CurrentActivity;
            var window = activity?.Window;
            if (window == null)
            {
                return;
            }

            if (!this.hasCapturedInitialState)
            {
                this.initialSoftInputMode = window.Attributes?.SoftInputMode ?? global::Android.Views.SoftInput.AdjustUnspecified;
                this.hasCapturedInitialState = true;
            }

            // Opt out of edge-to-edge so AdjustResize properly shrinks the window.
            AndroidX.Core.View.WindowCompat.SetDecorFitsSystemWindows(window, true);
            window.SetSoftInputMode(global::Android.Views.SoftInput.AdjustResize);
        }

        private void RestoreOriginalMode()
        {
            if (!this.hasCapturedInitialState)
            {
                return;
            }

            var activity = Microsoft.Maui.ApplicationModel.Platform.CurrentActivity;
            var window = activity?.Window;
            if (window == null)
            {
                return;
            }

            window.SetSoftInputMode(this.initialSoftInputMode);
            // Restore edge-to-edge.
            AndroidX.Core.View.WindowCompat.SetDecorFitsSystemWindows(window, false);
        }
#endif
    }
