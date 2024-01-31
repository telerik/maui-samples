using Microsoft.Maui.Controls;
using Microsoft.Maui.Devices;
using System.Diagnostics;
using AndroidSpecific = Microsoft.Maui.Controls.PlatformConfiguration.AndroidSpecific;

namespace QSF.Behaviors
{
    public class AndroidResizeSoftInputModeBehavior : Behavior<Element>
    {
        private AndroidSpecific.WindowSoftInputModeAdjust lastInputMode = AndroidSpecific.WindowSoftInputModeAdjust.Unspecified;

        protected override void OnAttachedTo(Element element)
        {
            base.OnAttachedTo(element);
#if ANDROID
            element.ParentChanged += this.Element_ParentChanged;
#endif
        }

        protected override void OnDetachingFrom(Element element)
        {
            base.OnDetachingFrom(element);
#if ANDROID
            element.ParentChanged -= this.Element_ParentChanged;
#endif
        }

        private void Element_ParentChanged(object sender, System.EventArgs e)
        {
            var element = (Element)sender;

            // ParentChanged (when Parent set to null) is not called, so unsibscribe here.
            element.ParentChanged -= this.Element_ParentChanged;

            if (DeviceInfo.Platform == DevicePlatform.Android)
            {
                Debug.WriteLine("ParentChanged: " + element.Parent);
                if (element.Parent != null)
                {
                    if (this.lastInputMode == AndroidSpecific.WindowSoftInputModeAdjust.Unspecified)
                    {
                        this.lastInputMode = GetSoftInputMode();
                    }

                    SetSoftInputMode(AndroidSpecific.WindowSoftInputModeAdjust.Resize);

                    var page = GetParentPage(element);
                    if (page != null)
                    {
                        page.NavigatedFrom += this.ParentPage_NavigatedFrom;
                    }
                }
                else
                {
                    SetSoftInputMode(this.lastInputMode);
                }
            }
        }

        private void ParentPage_NavigatedFrom(object sender, NavigatedFromEventArgs e)
        {
            var page = (Page)sender;

            // Behavior.OnDetachingFrom is not called, so we unsibscribe here and reset SoftInputMode.
            page.NavigatedFrom -= this.ParentPage_NavigatedFrom;
            SetSoftInputMode(this.lastInputMode);
        }

        private static Page GetParentPage(Element element)
        {
            var parentElement = element.Parent;

            while (parentElement != null)
            {
                var targetElement = parentElement as Page;

                if (targetElement != null)
                {
                    return targetElement;
                }

                parentElement = parentElement.Parent;
            }

            return null;
        }

        private static AndroidSpecific.WindowSoftInputModeAdjust GetSoftInputMode()
        {
            return AndroidSpecific.Application.GetWindowSoftInputModeAdjust(Application.Current);
        }

        private static void SetSoftInputMode(AndroidSpecific.WindowSoftInputModeAdjust inputMode)
        {
            AndroidSpecific.Application.SetWindowSoftInputModeAdjust(Application.Current, inputMode);
        }
    }
}
