using Microsoft.Maui.Controls;
using Microsoft.Maui.Controls.Xaml;
using Microsoft.Maui.Devices;
using Telerik.Maui.Controls;
using AndroidSpecific = Microsoft.Maui.Controls.PlatformConfiguration.AndroidSpecific;

namespace QSF.Examples.RichTextEditorControl.FirstLookExample;

[XamlCompilation(XamlCompilationOptions.Compile)]
public partial class FirstLookView : RadContentView
{
    private AndroidSpecific.WindowSoftInputModeAdjust lastInputMode = AndroidSpecific.WindowSoftInputModeAdjust.Unspecified;

    public FirstLookView()
    {
        this.InitializeComponent();
    }

    protected override void OnParentSet()
    {
        base.OnParentSet();
        if (DeviceInfo.Platform == DevicePlatform.Android)
        {
            if (this.Parent != null)
            {
                if (this.lastInputMode == AndroidSpecific.WindowSoftInputModeAdjust.Unspecified)
                {
                    this.lastInputMode = GetSoftInputMode();
                }

                SetSoftInputMode(AndroidSpecific.WindowSoftInputModeAdjust.Resize);
            }
            else
            {
                SetSoftInputMode(this.lastInputMode);
            }
        }
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
